namespace E_commerce_Service.Services
{
    public interface IInventoryService
    {
        Task UpdateInventoryAsync(int productId, int quantityChange);
    }
}
