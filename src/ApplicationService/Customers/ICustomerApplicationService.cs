using NearToEndpointDtos.Customers;

namespace ApplicationService.Customers
{
    public interface ICustomerApplicationService
    {
        Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto);
    }
}
