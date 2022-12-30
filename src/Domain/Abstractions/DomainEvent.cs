namespace Domain.Abstractions
{
    public abstract class DomainEvent 
    {
        public long Id { get; set; }
        public object Data { get; set; } = null!;
        public bool? IsPublished { get; set; }
        public bool? IsCallbackCompleted { get; set; }
        public DateTimeOffset DateTime { get; set; }

    }
}
