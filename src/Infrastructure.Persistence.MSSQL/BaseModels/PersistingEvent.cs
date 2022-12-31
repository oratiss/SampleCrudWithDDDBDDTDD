namespace Infrastructure.Persistence.MSSQL.BaseModels
{
    public class PersistingEvent
    {
        public bool? IsPublished { get; set; }
        public bool? IsCallbackCompleted { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
