using fosku_server.Helpers.Validation;
using fosku_server.Models;
using fosku_server.Services.Auth;
using fosku_server.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fosku_server.Controllers
{
    [ApiController]
    [Route("/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IAuthService authService;

        public CustomerController(ICustomerService customerService, IAuthService authService)
        {
            this.customerService = customerService;
            this.authService = authService;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(CheckTokenClaimsFilter))]
        public IActionResult GetCustomer(int CustomerId)
        {
            Customer? customer = customerService.GetCustomer(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
