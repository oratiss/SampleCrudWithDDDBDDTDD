namespace Infrastructure.Persistence.Mongo.Models
{
    public class BankAccount
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset UpdateDateTime { get; set; }
    }
}
