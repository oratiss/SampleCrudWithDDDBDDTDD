using AcceptanceTestsWithBDD.Abstractions;
using AcceptanceTestsWithBDD.TestBuilders.Customers;
using NearToEndpointDtos.Customers;
using System.Net.Http.Json;
using System.Text.Json;

namespace AcceptanceTestsWithBDD.StepDefinitions
{
    [Binding]
    public class CustomerStepDefinitions : BaseTest
    {
        private readonly AddCustomerDtoTestBuilder _addCustomerDtoTestBuilder = new AddCustomerDtoTestBuilder();
        private CustomerDto? _customerDto;

        public CustomerStepDefinitions()
        {
            if (client != null) client.BaseAddress = new Uri("https://localhost:7014/api/Customers");
        }

        [Given(@"\[FirstName is Masoud]")]
        public void GivenFirstNameIsMasoud()
        {
            _addCustomerDtoTestBuilder.With(x => x.FirstName, "Masoud");
        }

        [Given(@"\[LastName is Asgarian]")]
        public void GivenLastNameIsAsgarian()
        {
            _addCustomerDtoTestBuilder.With(x => x.LastName, "Asgarian");
        }


        [Given(@"\[DateOfBirth is '([^']*)']")]
        public void GivenDateOfBirthIs(string p0)
        {
            DateTime dateOfBirth = DateTime.Parse(p0);
            DateTimeOffset dateOfBirthOffset = new DateTimeOffset(dateOfBirth, new TimeSpan(0, 3, 30, 0));
            _addCustomerDtoTestBuilder.With(x => x.DateOfBirth, dateOfBirthOffset);
        }

        [Given(@"\[PhoneNumber is '([^']*)']")]
        public void GivenPhoneNumberIs(string p0)
        {
            _addCustomerDtoTestBuilder.With(x => x.PhoneNumber, p0);
        }

        [Given(@"\[Email is massoud\.asgarian@gmail\.com]")]
        public void GivenEmailIsMassoud_AsgarianGmail_Com()
        {
            _addCustomerDtoTestBuilder.With(x => x.Email, "Massoud.Asgarian@gmail.com");
        }

        [Given(@"\[BankAccountNumber is 0112345678]")]
        public void GivenBankAccountNumberIs()
        {
            _addCustomerDtoTestBuilder.With(x => x.BankAccountNumber, "0112345678");
        }

        [When(@"\[user is Added]")]
        public async Task WhenUserIsAdded()
        {
            AddCustomerDto addCustomerDto = _addCustomerDtoTestBuilder.Build();
            HttpResponseMessage response = await client?.PostAsJsonAsync(client?.BaseAddress, addCustomerDto)!;
            response.EnsureSuccessStatusCode();
            this._customerDto = await response.Content.ReadFromJsonAsync<CustomerDto>();
        }

        [Then(@"\[there should be user with above properties]")]
        public async Task ThenThereShouldBeUserWithAboveProperties()
        {
            CustomerDto? actualCustomerDto = await client!.GetFromJsonAsync<CustomerDto>($"{client!.BaseAddress}/{this!._customerDto!.Id}");
            actualCustomerDto.Should().NotBeNull();

            AddCustomerDto addCustomerDto = _addCustomerDtoTestBuilder.Build();
            CustomerDto expectedCustomerDto = new CustomerDtoTestBuilder()
                .With(x => x.Id, _customerDto.Id)
                .With(x => x.FirstName, addCustomerDto.FirstName.Trim().ToLower())
                .With(x => x.LastName, addCustomerDto.LastName.Trim().ToLower())
                .With(x => x.DateOfBirth, addCustomerDto.DateOfBirth)
                .With(x => x.PhoneNumber, addCustomerDto.PhoneNumber)
                .With(x => x.Email, addCustomerDto.Email.Trim().ToLower())
                .With(x => x.BankAccountNumber, addCustomerDto.BankAccountNumber)
                .Build();

            actualCustomerDto.Should().BeEquivalentTo(expectedCustomerDto);
        }












    }
}
