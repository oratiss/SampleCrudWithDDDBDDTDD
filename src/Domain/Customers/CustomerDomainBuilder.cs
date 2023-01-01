using Domain.Abstractions;
using DomainContract.Constants.Customers;
using Utility.ReflectionTools;

namespace Domain.Customers
{
    public class CustomerDomainBuilder : ReflectionBuilder<CustomerDomain, CustomerDomainBuilder>
    {
        private CustomerDomainBuilder _builderInstance;
        public string FirstName = CustomerConstants.SomeFirstName;
        public string LastName = CustomerConstants.SomeLastName;
        public DateTimeOffset DateOfBirth = CustomerConstants.SomeDateOfBirth;
        public string PhoneNumber = CustomerConstants.SomePhoneNumber;
        public string Email = CustomerConstants.SomeEmail;
        public BankAccount BankAccount = new BankAccount(CustomerConstants.SomeBankAccountNumber);

        public CustomerDomainBuilder()
        {
            _builderInstance = this;
        }

        public override CustomerDomain Build()
        {
            var customerDomain = new CustomerDomain(FirstName, LastName, DateOfBirth, PhoneNumber, Email, BankAccount.Number)
            {
                DomainEvents = new List<DomainEvent>()
                {
                    new CustomerCreatedEvent()
                    {
                        BankAccountNumber = BankAccount.Number,
                        IsPublished = false,
                        DateTime = DateTimeOffset.Now
                    }
                },
            };
            return customerDomain;
        }
    }
}
