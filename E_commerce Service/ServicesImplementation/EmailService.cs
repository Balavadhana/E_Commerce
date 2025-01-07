using E_commerce_Service.Services;

namespace E_commerce_Service.ServicesImplementation
{
    public class EmailService : IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(string customerEmail, int orderId)
        {
            
            Console.WriteLine($"Sending order confirmation email to {customerEmail} for Order ID: {orderId}");
            return Task.CompletedTask;
        }
    }
}
