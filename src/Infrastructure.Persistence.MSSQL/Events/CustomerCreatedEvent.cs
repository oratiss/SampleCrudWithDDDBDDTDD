using Infrastructure.Persistence.MSSQL.BaseModels;
using Infrastructure.Persistence.MSSQL.Models;

namespace Infrastructure.Persistence.MSSQL.Events
{
    public class CustomerCreatedEvent : PersistingEvent
    {
        public long CustomerId { get; set; }
        public string BankAccountNumber { get; set; } = null!;

        public Customer Customer { get; set; } = null!;
    }
}
