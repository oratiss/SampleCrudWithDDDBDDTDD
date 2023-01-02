using NearToEndpointDtos.Customers;

namespace ApplicationService.Customers
{
    public interface ICustomerApplicationService
    {
        Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto);
        Task<CustomerDto> Get(long key);
    }
}
