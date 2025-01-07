using E_commerce_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
        public class OrderController : ControllerBase
        {
            private readonly IOrderService _orderService;
            private readonly IInventoryService _inventoryService;
            private readonly IEmailService _emailService;

            public OrderController(IOrderService orderService, IInventoryService inventoryService, IEmailService emailService)
            {
                _orderService = orderService;
                _inventoryService = inventoryService;
                _emailService = emailService;
            }


            
            [HttpPost("process")]
            public async Task<IActionResult> ProcessOrders()
            {
                
                var pendingOrders = await _orderService.GetPendingOrdersAsync();

                if (pendingOrders == null || pendingOrders.Count == 0)
                {
                    return NoContent(); 
                }

                foreach (var order in pendingOrders)
                {
                    
                    foreach (var item in order.Items)
                    {
                        await _inventoryService.UpdateInventoryAsync(item.ProductId, -item.Quantity);
                    }

                    
                    await _emailService.SendOrderConfirmationEmailAsync(order.CustomerEmail, order.OrderId);

                
                    await _orderService.MarkOrderAsProcessedAsync(order.OrderId);
                }

                return Ok("Orders processed successfully");
            }
        }
    }
