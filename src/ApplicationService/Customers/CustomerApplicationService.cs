using NearToEndpointDtos.Customers;

namespace ApplicationService.Customers
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        //todo: wil be completed after writing domain layer with TDD and implementing repositories
        //private readonly IReadableCustomerRespositoryService _readableCustomerRespositoryService;
        //private readonly IWritableCustomerRepositoryService _writableCustomerRepositoryService;
        //private readonly ICustomerDomain _customerDomain;
        
        //public async Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto)
        //{
        //    var customer = await _readableCustomerRespositoryService.GetCustomerAsync(x=>x.FirstName == addCustomerDto.FirstName 
        //        && x.LastName == addCustomerDto.LastName
        //        && x.DateOfBirth == addCustomerDto.DateOfBirth);
        //    if (customer != null)
        //    {
        //        throw new ArgumentException("there is a customer with same first name, last name and date of birth");
        //    }

        //    customer = await _readableCustomerRespositoryService.GetCustomerAsync(x =>
        //        x.Email == addCustomerDto.Email.Trim().ToLower());
        //    if (customer != null)
        //    {
        //        throw new ArgumentException("The given email is already registered.");
        //    }

        //    //map addCustomerDto to domainDto
        //    //pass domain dto to customerDomainBuilder
        //    //if there is any exception we will return exception

        //    //in domain layer we create domain event for bank account number and source it.
        //    //after that we pass domain object to  application layer, convert it to repo entities, store entity in repositories and dispatch events
        //    //on call back of events for example bank account callback,we will update our value object of bank account number

        //    //if everything goes well 

        //    return null;
        public Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto)
        {
            throw new NotImplementedException();
        }
    }
}
