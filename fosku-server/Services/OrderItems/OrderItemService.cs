using fosku_server.Data;
using fosku_server.Models;

namespace fosku_server.Services.OrderItems
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;
        public OrderItemService(AppDbContext context) 
        {
            _context = context;
        }

        public OrderItem? GetOrderItem(int id)
        {
            var query = from obj in _context.OrderItems
                        where obj.Id == id
                        select obj;
            OrderItem? Obj = query.FirstOrDefault();
            return Obj;
        }

        public void CreateOrderItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
        }

        public void UpdateOrderItem(OrderItem item)
        {
            _context.OrderItems.Update(item);
        }
    }
}
