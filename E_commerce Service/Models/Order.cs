namespace E_commerce_Service.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public string Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
