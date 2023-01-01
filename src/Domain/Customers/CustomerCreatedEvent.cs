using Domain.Abstractions;

namespace Domain.Customers
{
    public class CustomerCreatedEvent: DomainEvent
    {
        public string BankAccountNumber { get; set; } = null!;
    }
}
