namespace Domain.Abstractions
{
    //just a markup class
    public abstract class AggregateRoot
    {
        public List<DomainEvent>? DomainEvents { get; set; } 
    }
}
