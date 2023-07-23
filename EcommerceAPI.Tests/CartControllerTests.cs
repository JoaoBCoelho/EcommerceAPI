namespace EcommerceAPI.Tests
{
    public class CartControllerTests
    {
        private readonly Mock<ICartService> _cartServiceMock;
        private readonly CartController _cartController;

        public CartControllerTests()
        {
            _cartServiceMock = new Mock<ICartService>();
            _cartController = new CartController(_cartServiceMock.Object);
        }

        [Fact]
        public async Task Get_WithExistingCart_ReturnsOkResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new CartDTO { Id = cartId };
            _cartServiceMock.Setup(service => service.GetAsync(cartId)).ReturnsAsync(cart);

            // Act
            var result = await _cartController.Get(cartId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCart = Assert.IsAssignableFrom<CartDTO>(okResult.Value);
            Assert.Equal(cart, returnedCart);
        }

        [Fact]
        public async Task Get_WithNonexistentCart_ReturnsNoContentResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            _cartServiceMock.Setup(service => service.GetAsync(cartId)).ReturnsAsync((CartDTO)null);

            // Act
            var result = await _cartController.Get(cartId);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task Create_ValidCart_ReturnsCreatedResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            _cartServiceMock.Setup(service => service.CreateAsync()).ReturnsAsync(cartId);

            // Act
            var result = await _cartController.Create();

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            Assert.NotNull(createdResult.Value);
        }

        [Fact]
        public async Task AddProductToCart_ValidProduct_ReturnsNoContentResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 1;

            // Act
            var result = await _cartController.AddProductToCart(cartId, productId, quantity);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _cartServiceMock.Verify(service => service.AddToCartAsync(cartId, productId, quantity), Times.Once);
        }

        [Fact]
        public async Task UpdateProductFromCart_ValidProduct_ReturnsNoContentResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 2;

            // Act
            var result = await _cartController.UpdateProductFromCart(cartId, productId, quantity);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _cartServiceMock.Verify(service => service.UpdateCartAsync(cartId, productId, quantity), Times.Once);
        }

        [Fact]
        public async Task RemoveProductFromCart_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            // Act
            var result = await _cartController.RemoveProductFromCart(cartId, productId);

            // Assert
            Assert.IsType<OkResult>(result);
            _cartServiceMock.Verify(service => service.RemoveFromCartAsync(cartId, productId), Times.Once);
        }
    }
}
