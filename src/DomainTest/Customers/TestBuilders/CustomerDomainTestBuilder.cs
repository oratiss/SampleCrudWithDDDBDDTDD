using Domain.Customers;
using DomainContract.Constants.Customers;
using Utility.ReflectionTools;

namespace DomainTest.Customers.TestBuilders
{
    public class CustomerDomainTestBuilder:ReflectionBuilder<CustomerDomain, CustomerDomainTestBuilder >
    {
        private CustomerDomainTestBuilder _builderInstance;

        public string FirstName = CustomerConstants.SomeFirstName;
        public string LastName = CustomerConstants.SomeLastName;
        public DateTimeOffset DateOfBirth = CustomerConstants.SomeDateOfBirth;
        public string PhoneNumber = CustomerConstants.SomePhoneNumber;
        public string Email = CustomerConstants.SomeEmail;
        public string BankAccountNumber = CustomerConstants.SomeBankAccountNumber;

        public CustomerDomainTestBuilder()
        {
            _builderInstance = this;
        }

        public override CustomerDomain Build()
        {
            var customerDomain = new CustomerDomain(this.FirstName, this.LastName, this.DateOfBirth, this.PhoneNumber,
                this.Email, this.BankAccountNumber);
            return customerDomain;
        }
    }
}
