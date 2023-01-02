using ApplicationService.Customers;
using Microsoft.AspNetCore.Mvc;
using NearToEndpointDtos.Customers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerApplicationService _CustomerApplicationService;

        public CustomersController(ICustomerApplicationService customerApplicationService)
        {
            _CustomerApplicationService = (CustomerApplicationService)customerApplicationService;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var customer = await _CustomerApplicationService.Get(id);
            if (!string.IsNullOrWhiteSpace(customer.ErrorMessage))
            {
                return NotFound(customer.ErrorMessage);
            }
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCustomerDto addCustomerDto)
        {
            var customer = await _CustomerApplicationService.AddCustomerAsync(addCustomerDto);
            if (!string.IsNullOrWhiteSpace(customer.ErrorMessage))
            {
                return Problem(customer.ErrorMessage);
            }

            return Ok(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
