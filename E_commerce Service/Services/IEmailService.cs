namespace E_commerce_Service.Services
{
    public interface IEmailService
    {
        Task SendOrderConfirmationEmailAsync(string customerEmail, int orderId);
    }
}
