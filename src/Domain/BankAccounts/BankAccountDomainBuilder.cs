using DomainContract.Constants.Customers;
using Utility.ReflectionTools;

namespace Domain.BankAccounts
{
    public class BankAccountDomainBuilder:ReflectionBuilder<BankAccountDomain, BankAccountDomainBuilder>
    {
        private readonly BankAccountDomainBuilder _builderInstance;
        public string AccountNumber = CustomerConstants.SomeBankAccountNumber;
        public DateTimeOffset CreateDateTime = CustomerConstants.SomeCreateDateTime;
        public DateTimeOffset? UpdateDateTime = CustomerConstants.SomeUpdateDateTime;

        public CustomerVo CustomerVo = new CustomerVo(CustomerConstants.SomeId,
            $"{CustomerConstants.SomeFirstName} {CustomerConstants.SomeLastName}", CustomerConstants.SomeDateOfBirth);

        public BankAccountDomainBuilder()
        {
            _builderInstance = this;
        }
        public override BankAccountDomain Build()
        {
            var bankAccountDomain = new BankAccountDomain(AccountNumber, CreateDateTime, UpdateDateTime, CustomerVo);
            return bankAccountDomain;
        }
    }
}
