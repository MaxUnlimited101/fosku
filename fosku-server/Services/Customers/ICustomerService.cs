using fosku_server.Models;

namespace fosku_server.Services.Customers;

public interface ICustomerService
{
    Customer? GetCustomer(int Id);
    Customer? GetCustomer(string Email);
    void CreateCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
}