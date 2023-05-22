namespace EcommerceAPI.Tests
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _orderController = new OrderController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task Get_WithExistingOrder_ReturnsOkResult()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new OrderDTO { Id = orderId };
            _orderServiceMock.Setup(service => service.GetAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderController.Get(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsAssignableFrom<OrderDTO>(okResult.Value);
            Assert.Equal(order, returnedOrder);
        }

        [Fact]
        public async Task Get_WithNonexistentOrder_ReturnsNoContentResult()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderServiceMock.Setup(service => service.GetAsync(orderId)).ReturnsAsync((OrderDTO)null);

            // Act
            var result = await _orderController.Get(orderId);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}
