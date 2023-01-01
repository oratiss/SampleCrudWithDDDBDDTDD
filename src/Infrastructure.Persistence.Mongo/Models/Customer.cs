﻿namespace Infrastructure.Persistence.Mongo.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        //value objects
        public BankAccount BankAccount { get; set; } = null!;
    }
}