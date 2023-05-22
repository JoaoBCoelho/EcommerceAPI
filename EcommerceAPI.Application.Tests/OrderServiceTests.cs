namespace EcommerceAPI.Application.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAsync_ValidId_ReturnsOrderDTO()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Order order = new Order();
            OrderDTO orderDTO = new OrderDTO();
            _orderRepositoryMock.Setup(repo => repo.GetAsync(orderId)).ReturnsAsync(order);
            _mapperMock.Setup(mapper => mapper.Map<OrderDTO>(order)).Returns(orderDTO);

            // Act
            OrderDTO result = await _orderService.GetAsync(orderId);

            // Assert
            Assert.Equal(orderDTO, result);
        }
    }
}