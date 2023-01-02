using Domain.Customers;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;
using Mapster;
using NearToEndpointDtos.Customers;
using BankAccount = Domain.Customers.BankAccount;
using CustomerCreatedEvent = Infrastructure.Persistence.MSSQL.Events.CustomerCreatedEvent;
using ReadableCustomer = Infrastructure.Persistence.Mongo.Models.Customer;
using WritableCustomer = Infrastructure.Persistence.MSSQL.Models.Customer;

namespace ApplicationService.Customers
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        //todo: wil be completed after writing domain layer with TDD and implementing repositories
        private readonly ICustomerReadableRepository _customerReadableRepository;
        private readonly ICustomerWritableRepository _customerWritableRepository;

        public CustomerApplicationService(ICustomerReadableRepository customerReadableRepository,
            ICustomerWritableRepository customerWritableRepository)
        {
            _customerReadableRepository = customerReadableRepository;
            _customerWritableRepository = customerWritableRepository;
        }

        public async Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto)
        {
            var customers = await _customerReadableRepository.SearchForPeople(x => x.FirstName == addCustomerDto.FirstName
                && x.LastName == addCustomerDto.LastName
                && x.DateOfBirth == addCustomerDto.DateOfBirth, x => x.Id);
            if (customers.Any())
            {
                throw new ArgumentException("there is a customer with same first name, last name and date of birth");
            }

            customers = await _customerReadableRepository.SearchForPeople(x => x.Email == addCustomerDto.Email.Trim().ToLower(), x => x.Id);
            if (customers.Any())
            {
                throw new ArgumentException("The given email is already registered for another account.");
            }

            CustomerDomain? addingCustomerDomain = null;
            try
            {
                addingCustomerDomain = new CustomerDomainBuilder()
                   .With(x => x.FirstName, addCustomerDto.FirstName)
                   .With(x => x.LastName, addCustomerDto.LastName)
                   .With(x => x.DateOfBirth, addCustomerDto.DateOfBirth)
                   .With(x => x.PhoneNumber, addCustomerDto.PhoneNumber)
                   .With(x => x.Email, addCustomerDto.Email)
                   .With(x => x.BankAccount, new BankAccount(addCustomerDto.BankAccountNumber))
                   .Build();

                var persistingCustomerForWriteSide = addingCustomerDomain.Adapt<WritableCustomer>();
                persistingCustomerForWriteSide.CustomerCreatedEvent = addingCustomerDomain.DomainEvents?.SingleOrDefault()?.Adapt<CustomerCreatedEvent>();

                var addedToWriteDbCustomer = await _customerWritableRepository.Add(persistingCustomerForWriteSide);

                var persistingCustomerForReadSide = addingCustomerDomain.Adapt<ReadableCustomer>();
                persistingCustomerForReadSide.Id = addedToWriteDbCustomer.Id;
                var addedToReadDbCustomer = await _customerReadableRepository.Add(persistingCustomerForReadSide);

                var outputCustomerDto = addedToWriteDbCustomer.Adapt<CustomerDto>();
                return outputCustomerDto;
            }
            catch (Exception e)
            {
                return new CustomerDto()
                {
                    ErrorMessage = e.Message
                };
            }


            //    //on call back of events for example bank account callback,we will update our value object of bank account number
            // and insert new events in database with Is callback completed
        }

        public async Task<CustomerDto> Get(long key)
        {
            ReadableCustomer customer = await _customerReadableRepository.Get(key);
            CustomerDto customerDto = customer.Adapt<CustomerDto>();
            return customerDto;
        }
    }
}
