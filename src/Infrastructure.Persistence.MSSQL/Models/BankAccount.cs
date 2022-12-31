
namespace Infrastructure.Persistence.MSSQL.Models
{
    public class BankAccount
    {
        public string Number { get; set; } = null!;
        public long CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
