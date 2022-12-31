using Domain.Customers;
using DomainTest.Customers.TestBuilders;
using FluentAssertions;

namespace DomainTest.Customers
{
    //Todo: avoid using hard code messages for exceptions. try using constants.
    public class CustomerTest
    {
        [Fact]
        public void FirstName_Should_Not_Be_NullOrWhiteSpace()
        {
            Action act = () =>
            {
                var customer = new CustomerDomainTestBuilder()
                    .With(x => x.FirstName, string.Empty)
                    .Build();
            };
            act.Should().Throw<Exception>().WithMessage("First name is null,empty or white space.");
        }

        [Fact]
        public void LastName_Should_Not_Be_NullOrWhiteSpace()
        {
            Action act = () =>
            {
                var customer = new CustomerDomainTestBuilder()
                    .With(x => x.LastName, string.Empty)
                    .Build();
            };
            act.Should().Throw<Exception>().WithMessage("Last name is null, empty or white space.");
        }

        [Fact]
        public void Customer_Should_Be_Equal_Or_Greater_Than_Twelve_years_Old()
        {
            Action act = () =>
            {
                var customer = new CustomerDomainTestBuilder()
                    .With(x => x.DateOfBirth, new DateTimeOffset(new DateTime(2011, 12, 25), new TimeSpan(0, 3, 30, 0)))
                    .Build();
            };
            act.Should().Throw<Exception>().WithMessage("Customer should be at least 12 years old.");
        }

        [Fact]
        public void PhoneNumber_Should_Not_Be_NullOrWhiteSpace()
        {
            Action act = () =>
            {
                var customer = new CustomerDomainTestBuilder()
                    .With(x => x.PhoneNumber, string.Empty)
                    .Build();
            };
            act.Should().Throw<Exception>().WithMessage("PhoneNumber is null, empty or white space.");
        }

        [Fact]
        public void Email_Should_Not_Be_NullOrWhiteSpace()
        {
            Action act = () =>
            {
                var customer = new CustomerDomainTestBuilder()
                    .With(x => x.Email, string.Empty)
                    .Build();
            };
            act.Should().Throw<Exception>().WithMessage("Email is null, empty or white space.");
        }

        [Fact]
        public void BankAcccountNumber_Should_Not_Be_NullOrWhiteSpace()
        {
            Action act = () =>
            {
                var customer = new CustomerDomainTestBuilder()
                    .With(x => x.BankAccount, new BankAccount(string.Empty))
                    .Build();
            };
            act.Should().Throw<Exception>().WithMessage("BankAccountNumber is null, empty or white space.");
        }


    }
}
