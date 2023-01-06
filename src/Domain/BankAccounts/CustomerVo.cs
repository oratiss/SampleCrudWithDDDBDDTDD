using Domain.Abstractions;

namespace Domain.BankAccounts
{
    public class CustomerVo: ValueObject
    {
        public long CustomerId { get; private set; }
        public string CustomerFullName { get; private set; } = null!;
        public DateTimeOffset DateOfBirth { get; private set; }

        public CustomerVo(long customerId, string customerFullName, DateTimeOffset dateOfBirth)
        {
            if (DateTimeOffset.Now.Year - DateOfBirth.Year < 12)
            {
                throw new Exception("Customer should be at least 12 years old.") { HResult = 30 };
            }

            if (string.IsNullOrWhiteSpace(CustomerFullName))
            {
                throw new Exception("Customer's full name is null,empty or white space.") { HResult = 10 };
            }

            CustomerId = customerId;
            CustomerFullName = customerFullName;
            DateOfBirth = dateOfBirth;
        }
    }
}
