namespace NearToEndpointDtos.BankAccounts
{
    public class BankAccountDto
    {
        public long Id { get; set; }
        public string BankAccountNumber { get; set; } = null!;
        public long CustomerId { get; set; }
        public string CustomerFullName { get; set; } = null!;
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset? UpdateDateTime { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
