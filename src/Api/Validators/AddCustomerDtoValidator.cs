using Api.Dtos;
using FluentValidation;
using PhoneNumbers;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Api.Validators
{
    public class AddCustomerDtoValidator : AbstractValidator<AddCustomerDto>
    {
        public AddCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("please specify a first name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a last name.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("please specify your date of birth.");
            RuleFor(x => x.PhoneNumber).Must(BeAValidPhoneNumber).WithMessage("Please enter your phone number starting with your country code including + sign at teh first. for example US code is +1. the format 001 at the start also is acceptable for US.");
            RuleFor(x => x.Email).Must(BeAValidEmailAddress).WithMessage("Please enter a valid email address.");
            RuleFor(x => x.BankAccountNumber).Must(BeAValidBankAccountNumber).WithMessage("Please enter a valid bank account number.");
        }

        private bool BeAValidBankAccountNumber(string bankAccountNumber)
        {
            Regex regex = new Regex("^[0-9]{99}$");
            if (regex.IsMatch(bankAccountNumber))
            {
                return true;
            }

            return false;
        }

        private bool BeAValidEmailAddress(string emailAddress)
        {
            try
            {
                MailAddress address = new MailAddress(emailAddress);
                bool isValid = (address.Address == emailAddress);
                return isValid;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool BeAValidPhoneNumber(string aNumber)
        {
            aNumber = aNumber.Trim();

            if (!aNumber.StartsWith("+"))
            {
                if (aNumber.StartsWith("00"))
                {
                    // Replace 00 at beginning with +
                    aNumber = "+" + aNumber.Remove(0, 2);
                }
                else
                {
                    return false;
                }
            }

            try
            {
                var phoneNumber = PhoneNumberUtil.GetInstance().Parse(aNumber, "IR");
                bool result = PhoneNumberUtil.GetInstance().IsValidNumber(phoneNumber);
                return result;
            }
            catch
            {
                return false;
            }
        }




    }
}
