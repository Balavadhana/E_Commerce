using E_commerce_Service.Models;

namespace E_commerce_Service.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetPendingOrdersAsync();
        Task MarkOrderAsProcessedAsync(int orderId);
    }
}
