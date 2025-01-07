using E_commerce_Service.Controllers;
using E_commerce_Service.Models;
using E_commerce_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EcommerceApi.Tests
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly Mock<IInventoryService> _mockInventoryService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            
            _mockOrderService = new Mock<IOrderService>();
            _mockInventoryService = new Mock<IInventoryService>();
            _mockEmailService = new Mock<IEmailService>();

            
            _controller = new OrderController(
                _mockOrderService.Object,
                _mockInventoryService.Object,
                _mockEmailService.Object
            );
        }

        [Fact]
        public async Task ProcessOrders_ShouldProcessPendingOrders()
        {
            // Arrange:
            var pendingOrders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    CustomerEmail = "customer1@example.com",
                    Status = "Pending",
                    Items = new List<OrderItem> { new OrderItem { ProductId = 1, Quantity = 2 } }
                }
            };

            _mockOrderService.Setup(o => o.GetPendingOrdersAsync())
                .ReturnsAsync(pendingOrders);

            _mockOrderService.Setup(o => o.MarkOrderAsProcessedAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            _mockInventoryService.Setup(i => i.UpdateInventoryAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            _mockEmailService.Setup(e => e.SendOrderConfirmationEmailAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ProcessOrders();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal("Orders processed successfully", okResult.Value);


            _mockInventoryService.Verify(i => i.UpdateInventoryAsync(1, -2), Times.Once);
            _mockEmailService.Verify(e => e.SendOrderConfirmationEmailAsync("customer1@example.com", 1), Times.Once);
            _mockOrderService.Verify(o => o.MarkOrderAsProcessedAsync(1), Times.Once);
        }

        [Fact]
        public async Task ProcessOrders_ShouldReturnNoContent_WhenNoPendingOrders()
        {
            // Arrange: 
            _mockOrderService.Setup(o => o.GetPendingOrdersAsync())
                .ReturnsAsync(new List<Order>());

            // Act: 
            var result = await _controller.ProcessOrders();

            // Assert: 
            Assert.IsType<NoContentResult>(result);
        }
    }
}
