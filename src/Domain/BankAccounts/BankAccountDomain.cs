using Domain.Abstractions;

namespace Domain.BankAccounts
{
    public class BankAccountDomain: AggregateRoot
    {
        public string AccountNumber { get; private set; } = null!;
        public DateTimeOffset CreateDateTime { get; private set; }
        public DateTimeOffset? UpdateDateTime { get; private set; }
        public CustomerVo CustomerVo { get; private set; } = null!;

        public BankAccountDomain(string accountNumber, DateTimeOffset createDateTime, DateTimeOffset? updateDateTime, CustomerVo customerVo)
        {

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new Exception("AccountNumber is null, empty or white space.") { HResult = 60 };
            }

            AccountNumber = accountNumber;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            CustomerVo = customerVo;
        }
    }
}
