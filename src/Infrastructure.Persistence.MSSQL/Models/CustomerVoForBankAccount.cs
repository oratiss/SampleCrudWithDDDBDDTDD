namespace Infrastructure.Persistence.MSSQL.Models
{
    public class CustomerVoForBankAccount
    {
        public long BankAccountId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
        public BankAccount BankAccount { get; set; } = null!;

    }
}
