using ApplicationService.BankAccounts;
using Infrastructure.Persistence.MSSQL.Contexts;
using Microsoft.EntityFrameworkCore;
using NearToEndpointDtos.BankAccounts;

namespace EventPublisherWorkerService
{
    public class EventPublisherWorker : BackgroundService
    {
        private readonly ILogger<EventPublisherWorker> _logger;
        private readonly SampleDbContext _dbContext;
        private readonly IBankAccountApplicationService _bankAccountApplicationService;

        public EventPublisherWorker(ILogger<EventPublisherWorker> logger, SampleDbContext dbContext, IBankAccountApplicationService bankAccountApplicationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _bankAccountApplicationService = bankAccountApplicationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var unpublishedCustomerCreatedEvents =
                    await _dbContext.Customers
                        .Select(x => new
                        {
                            CustomerFullName = $"{x.CustomerCreatedEvent!.Customer.FirstName} {x.CustomerCreatedEvent!.Customer.LastName}",
                            x.DateOfBirth,
                            AccountNumber = x.CustomerCreatedEvent.BankAccountNumber,
                            x.CustomerCreatedEvent.CustomerId,
                            x.CustomerCreatedEvent.IsPublished,
                        })
                        .Where(x => x.IsPublished != null && !x.IsPublished.Value)
                        .ToListAsync(cancellationToken: stoppingToken);

                if (unpublishedCustomerCreatedEvents.Any())
                {
                    foreach (var unpublishedEvent in unpublishedCustomerCreatedEvents)
                    {
                        AddBankAccountDto addBankAccountDto = new()
                        {
                            BankAccountNumber = unpublishedEvent.AccountNumber,
                            DateOdBirth = unpublishedEvent.DateOfBirth,
                            CreateDateTime = DateTimeOffset.Now,
                            CustomerFullName = unpublishedEvent.CustomerFullName,
                            CustomerId = unpublishedEvent.CustomerId
                        };
                        await _bankAccountApplicationService.AddBankAccountAsync(addBankAccountDto);

                    }

                    foreach (var unpublishedEvent in unpublishedCustomerCreatedEvents)
                    {
                        var customerCreatedEvent = _dbContext.Customers.FirstOrDefault(x =>
                            x.CustomerCreatedEvent!.CustomerId == unpublishedEvent.CustomerId);
                        customerCreatedEvent!.CustomerCreatedEvent!.IsPublished = true;
                        _dbContext.Update(customerCreatedEvent);
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken: stoppingToken);
                }

                Thread.Sleep(300000); //Wait 5 minutes
            }
        }
    }
}