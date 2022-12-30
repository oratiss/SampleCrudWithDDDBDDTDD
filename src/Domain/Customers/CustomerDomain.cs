using Domain.Abstractions;

namespace Domain.Customers
{
    public class CustomerDomain: AggregateRoot
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
                //Todo: adding exception caught by domain contract for exceptions + implementation of contract in domain service of Exception
                //throw new 
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
