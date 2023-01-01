namespace Domain.Abstractions
{
    public abstract class DomainEvent 
    {
        public bool? IsPublished { get; set; }
        public bool? IsCallbackCompleted { get; set; }
        public DateTimeOffset DateTime { get; set; }

    }
}
