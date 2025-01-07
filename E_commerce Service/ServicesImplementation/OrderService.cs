using E_commerce_Service.Models;
using E_commerce_Service.Services;

namespace E_commerce_Service.ServicesImplementation
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders;

        public OrderService()
        {
            // This would typically be a database or external service
            _orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerEmail = "customer1@example.com", Status = "Pending", Items = new List<OrderItem> { new OrderItem { ProductId = 1, Quantity = 2 } } },
            new Order { OrderId = 2, CustomerEmail = "customer2@example.com", Status = "Pending", Items = new List<OrderItem> { new OrderItem { ProductId = 2, Quantity = 1 } } }
        };
        }
        public Task<List<Order>> GetPendingOrdersAsync()
        {
            return Task.FromResult(_orders.Where(o => o.Status == "Pending").ToList());
        }

        public Task MarkOrderAsProcessedAsync(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = "Processed";
            }
            return Task.CompletedTask;
        }
    }
}
