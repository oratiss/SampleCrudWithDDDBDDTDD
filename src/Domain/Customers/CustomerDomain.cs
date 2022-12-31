using Domain.Abstractions;

namespace Domain.Customers
{
    public class CustomerDomain : AggregateRoot
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public DateTimeOffset DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public BankAccount BankAccount { get; private set; } = null!;

        public CustomerDomain(string firstName, string lastName, DateTimeOffset dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception("First name is null,empty or white space.") { HResult = 10 };
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("Last name is null,empty or white space.") { HResult = 20 };
            }

            if (DateTimeOffset.Now.Year - DateOfBirth.Year < 12)
            {
                throw new Exception("Customer should be at least 12 years old.") { HResult = 30 };
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new Exception("PhoneNumber is null, empty or white space.") { HResult = 40 };
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email is null, empty or white space.") { HResult = 50 };
            }

            if (string.IsNullOrWhiteSpace(bankAccountNumber))
            {
                throw new Exception("BankAccountNumber is null, empty or white space.") { HResult = 60 };
            }

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccount = new BankAccount(bankAccountNumber);
        }

    }
}
