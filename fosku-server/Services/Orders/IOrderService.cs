using fosku_server.Models;

namespace fosku_server.Services.Orders
{
    public interface IOrderService
    {
        public Order? GetOrder(int id);
        public void CreateOrder(Order order);
        public void UpdateOrder(Order order);
    }
}
