using AcceptanceTestsWithBDD.Constants.Customers;
using NearToEndpointDtos.Customers;
using Utility.ReflectionTools;

namespace AcceptanceTestsWithBDD.TestBuilders.Customers
{
    public class AddCustomerDtoTestBuilder : ReflectionBuilder<AddCustomerDto, AddCustomerDtoTestBuilder>
    {
        private AddCustomerDtoTestBuilder _builderInstance;
        public string FirstName = CustomerConstants.SomeFirstName;
        public string LastName = CustomerConstants.SomeLastName;
        public DateTimeOffset DateOfBirth = CustomerConstants.SomeDateOfBirth;
        public string PhoneNumber = CustomerConstants.SomePhoneNumber;
        public string Email = CustomerConstants.SomeEmail;
        public string BankAccountNumber = CustomerConstants.SomeBankAccountNumber;

        public AddCustomerDtoTestBuilder()
        {
            _builderInstance = this;
        }

        public override AddCustomerDto Build()
        {
            AddCustomerDto addCustomerDto = new()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                BankAccountNumber = this.BankAccountNumber,
            };
            return addCustomerDto;
        }
    }
}
