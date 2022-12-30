using Domain.Abstractions;

namespace Domain.Customers
{
    public class BankAccount: ValueObject
    {
        public string Number { get; private set; } = null!;

        public BankAccount(string number)
        {
            Number = number;
        }
    }
}
