using Domain.Abstractions;

namespace Domain.Customers
{
    public class BankAccountVo: ValueObject
    {
        public string Number { get; private set; } = null!;

        public BankAccountVo(string number)
        {
            Number = number;
        }
    }
}
