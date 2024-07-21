using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orange_task.Domain.IService;

namespace Orange_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }



        [HttpGet]   // made default values for pagination  there are several ways better than this
        public async Task<IActionResult> GetCustomers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _customerService.GetCustomersAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("customer-expired-contract")]
        public async Task<IActionResult> GetCustomersWithExpiredContracts()
        {
            var result = await _customerService.GetCustomersWithExpiredContractsAsync();
            return Ok(result);
        }

        [HttpGet("expiring-soon")]
        public async Task<IActionResult> GetCustomersWithContractsExpiringWithinOneMonth()
        {
            var result = await _customerService.GetCustomersWithContractsExpiringWithinOneMonthAsync();
            return Ok(result);
        }

        [HttpGet("customers-count-per-service")]
        public async Task<IActionResult> GetCustomerCountsByServiceType()
        {
            var result = await _customerService.GetCustomerCountsByServiceTypeAsync();
            return Ok(result);
        }

        [HttpGet("monthly-customers-count-per-year")]
        public async Task<IActionResult> GetCustomerCountPerMonthPerYear()
        {
            var result = await _customerService.GetCustomerCountPerMonthPerYearAsync();
            return Ok(result);
        }
    }
}
