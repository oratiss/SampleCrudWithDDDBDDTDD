using AcceptanceTestsWithBDD.Constants.Customers;
using Api.Dtos;
using Utility.ReflectionTools;

namespace AcceptanceTestsWithBDD.TestBuilders.Customers
{
    public class CustomerDtoTestBuilder: ReflectionBuilder<CustomerDto, CustomerDtoTestBuilder>
    {
        private CustomerDtoTestBuilder _builderInstance;
        public long Id = CustomerConstants.SomeId;
        public string FirstName = CustomerConstants.SomeFirstName;
        public string LastName = CustomerConstants.SomeLastName;
        public DateTimeOffset DateOfBirth = CustomerConstants.SomeDateOfBirth;
        public string PhoneNumber = CustomerConstants.SomePhoneNumber;
        public string Email = CustomerConstants.SomeEmail;
        public string BankAccountNumber = CustomerConstants.SomeBankAccountNumber;

        public CustomerDtoTestBuilder()
        {
            _builderInstance = this;
        }

        public override CustomerDto Build()
        {
            CustomerDto customerDto = new()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                BankAccountNumber = this.BankAccountNumber
            };
            return customerDto;
        }
    }
}
