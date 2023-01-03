using Infrastructure.Persistence.MSSQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EventPublisherWorkerService
{
    public class EventPublisherWorker : BackgroundService
    {
        private readonly ILogger<EventPublisherWorker> _logger;
        private readonly SampleDbContext _dbContext;

        public EventPublisherWorker(ILogger<EventPublisherWorker> logger, SampleDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var unpublishedCustomerCreatedEvents =
                    await _dbContext.Customers
                        .Select(x => x.CustomerCreatedEvent)
                        .Where(x=>x.IsPublished != null && !x.IsPublished.Value)
                        .ToListAsync(cancellationToken: stoppingToken);

                if (unpublishedCustomerCreatedEvents.Any())
                {
                    foreach (var unpublishedEvent in unpublishedCustomerCreatedEvents)
                    {
                        //Todo: make AddBankAccount with UserId and user value objects from unpublishedEvent and pass it to BankApplicationService
                        //Todo: on success result, update event and resource it in db
                    }
                }

                await Task.Delay(15000, stoppingToken);
            }
        }
    }
}