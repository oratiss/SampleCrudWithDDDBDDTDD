using Infrastructure.Persistence.MSSQL.BaseModels;

namespace Infrastructure.Persistence.MSSQL.Models
{
    public class BankAccount: PersistingEntity<long>
    {
        public string AccountNumber { get; set; } = null!;
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset? UpdateDateTime { get; set; }
        public CustomerVoForBankAccount CustomerVoForBankAccount { get; set; } = null!;
    }
}
