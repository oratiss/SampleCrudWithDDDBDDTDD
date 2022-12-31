using Infrastructure.Persistence.MSSQL.BaseModels;
using Infrastructure.Persistence.MSSQL.Events;

namespace Infrastructure.Persistence.MSSQL.Models
{
    public class Customer: PersistingEntity<long>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        
        //value objects
        public BankAccount BankAccount { get; set; } = null!;

        //domain events as value object
        public CustomerCreatedEvent? CustomerCreatedEvent { get; set; }
    }
}
