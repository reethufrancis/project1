using Customer.Service;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace Training.CustomerAPI.Web.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create/{countrycode}")]
        public async Task<IActionResult> CreateCustomer(
            [FromBody] CustomerBase customerData,
            [FromRoute] string countrycode,
            ILogger<CustomerController> log)
        {
            try
            {
                if (!string.IsNullOrEmpty(countrycode) &&
                    (countrycode == "GB" || countrycode == "IT"))
                {
                    ICustomer customer = null;

                    if (countrycode == "GB")
                    {
                        customer = customerData as Customer_GB<Address>;
                    }
                    else if (countrycode == "IT")
                    {
                        customer = customerData as Customer_IT<AddressIT>;
                    }

                    if (customer == null)
                    {
                        return BadRequest("Invalid country code or customer data.");
                    }

                    await _customerService.CreateCustomerAsync(customer);

                    return Ok(customer);
                }
                else
                {
                    return BadRequest("Invalid country code.");
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An error occurred while processing the request.");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}





