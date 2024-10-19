using fosku_server.Data;
using fosku_server.DTO;
using fosku_server.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/customer")]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _context;
    public CustomerController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public Task<IAsyncResult> LoginCustomer([FromBody]CustomerLoginDTO model)
    {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public Task<IAsyncResult> RegisterCustomer()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task GetCustomer([FromBody]int id)
    {
        Customer customer = _context.Customers.Find(id);
    }
}