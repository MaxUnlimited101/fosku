using fosku_server.Models;

namespace fosku_server.Services.Customers
{
    public interface ICustomerService
    {
        public void CreateCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public Customer? GetCustomer(int id);
        public Customer? GetCustomer(string Email);

    }
}
