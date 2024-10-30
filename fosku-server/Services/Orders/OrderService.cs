using fosku_server.Data;
using fosku_server.Models;

namespace fosku_server.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Order? GetOrder(int id)
        {
            var query = from order in _context.Orders
                        where order.Id == id
                        select order;
            Order? ord = query.FirstOrDefault();
            return ord;
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
