using Domain.Customers;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using WritableCustomer = Infrastructure.Persistence.MSSQL.Models.Customer;
using ReadableCustomer = Infrastructure.Persistence.Mongo.Models.Customer;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;
using Mapster;
using NearToEndpointDtos.Customers;
using BankAccount = Domain.Customers.BankAccount;

namespace ApplicationService.Customers
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        //todo: wil be completed after writing domain layer with TDD and implementing repositories
        private readonly ICustomerReadableRepository _customerReadableRepository;
        private readonly ICustomerWritableRepository _customerWritableRepository;
        private readonly CustomerDomain _customerDomain;

        public CustomerApplicationService(ICustomerReadableRepository customerReadableRepository,
            ICustomerWritableRepository customerWritableRepository, CustomerDomain customerDomain)
        {
            _customerReadableRepository = customerReadableRepository;
            _customerWritableRepository = customerWritableRepository;
            _customerDomain = customerDomain;
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
                var addedToWriteDbCustomer = await _customerWritableRepository.Add(persistingCustomerForWriteSide);
                await _customerWritableRepository.Save();

                var persistingCustomerForReadSide = addingCustomerDomain.Adapt<ReadableCustomer>();
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

    }
}
