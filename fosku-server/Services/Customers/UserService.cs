using fosku_server.Models;
using fosku_server.Data;
using fosku_server.Helpers;

namespace fosku_server.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public void CreateCustomer(Customer customer)
    {
        byte[] saltBytes = HashingHelper.GenerateSalt();
        string hashPassword = HashingHelper.HashPassword(customer.PasswordHash, saltBytes);
        customer.SaltString = Convert.ToBase64String(saltBytes);
        customer.PasswordHash = hashPassword;
        _context.Customers.Add(customer);
        _context.SaveChangesAsync();
    }
    

    public Customer? GetCustomer(int id)
    {
        var customersQuery = from customers in _context.Customers
                        where customers.Id == id
                        select customers;
        Customer? Customer = customersQuery.FirstOrDefault();
        return Customer;
    }
    public Customer? GetCustomer(string Email)
    {
        var customersQuery = from customers in _context.Customers
                        where customers.Email == Email
                        select customers;
        Customer? Customer = customersQuery.FirstOrDefault();
        return Customer;
    }
    public void UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChangesAsync();
    }
}
