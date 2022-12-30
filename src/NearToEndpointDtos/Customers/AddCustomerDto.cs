namespace NearToEndpointDtos.Customers
{
    public class AddCustomerDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string BankAccountNumber { get; set; } = null!;
    }
}
