Feature: Customer

We are in Adding page of a user

@tag1
Scenario: [Adding A User with no previous data]
	Given [FirstName is Masoud]
	And [LastName is Asgarian]
	And [DateOfBirth is '1985-07-18']
	And [PhoneNumber is '+989120031084']
	And [Email is massoud.asgarian@gmail.com]
	And [BankAccountNumber is 0112345678]
	When [user is Added]
	Then [there should be user with above properties]
