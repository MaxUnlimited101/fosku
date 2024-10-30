using fosku_server.Models;

namespace fosku_server.Services.OrderItems
{
    public interface IOrderItemService
    {
        public OrderItem? GetOrderItem(int id);
        public void CreateOrderItem(OrderItem item);
        public void UpdateOrderItem(OrderItem item);
    }
}
