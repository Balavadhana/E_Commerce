using E_commerce_Service.Services;

namespace E_commerce_Service.ServicesImplementation
{
    public class InventoryService : IInventoryService
    {
        private readonly Dictionary<int, int> _inventory;

        public InventoryService()
        {
            
            _inventory = new Dictionary<int, int>
        {
            { 1, 100 }, // Product 1 has 100 units
            { 2, 50 }   // Product 2 has 50 units
        };
        }

        public Task UpdateInventoryAsync(int productId, int quantityChange)
        {
            if (_inventory.ContainsKey(productId))
            {
                _inventory[productId] += quantityChange;
            }
            return Task.CompletedTask;
        }
    }
}
