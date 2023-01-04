
namespace Infrastructure.Persistence.MSSQL.Models
{
    public class BankAccountVoForCustomer
    {
        public string Number { get; set; } = null!;
        public long CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
