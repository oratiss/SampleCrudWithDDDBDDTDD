namespace NearToEndpointDtos.BankAccounts
{
    public class AddBankAccountDto
    {
        public string BankAccountNumber { get; set; } = null!;
        public long CustomerId { get; set; }
        public string CustomerFullName { get; set; } = null!;
        public DateTimeOffset DateOdBirth { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset? UpdateDateTime { get; set; }
    }
}
