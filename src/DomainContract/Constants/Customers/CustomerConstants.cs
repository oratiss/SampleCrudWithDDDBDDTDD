namespace DomainContract.Constants.Customers
{
    public class CustomerConstants
    {
        public const long SomeId = 121; 
        public const string SomeFirstName = "someFirstNAme";
        public const string SomeLastName = "someLastName";
        public static DateTimeOffset SomeDateOfBirth =
            new DateTimeOffset(1947, 3, 21, 18, 30, 00, new TimeSpan(0, 3, 30, 0));
        public static DateTimeOffset SomeCreateDateTime =
            new DateTimeOffset(2023, 1, 3, 18, 30, 00, new TimeSpan(0, 3, 30, 0));

        public static DateTimeOffset? SomeUpdateDateTime = null;
        public const string SomePhoneNumber = "+989123245678";
        public const string SomeEmail = "someEmail@someHost.com";
        public const string SomeBankAccountNumber = "0114785236";
    }
}
