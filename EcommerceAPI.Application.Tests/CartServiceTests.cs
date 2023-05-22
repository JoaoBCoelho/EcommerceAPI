using EcommerceAPI.Application.Mappings;
using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Application.Tests
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            _cartService = new CartService(_cartRepositoryMock.Object, _productRepositoryMock.Object, _orderRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task CreateAsync_ValidInput_ReturnsCartId()
        {
            // Arrange
            Guid cartId = Guid.NewGuid();
            _cartRepositoryMock.Setup(repo => repo.CreateAsync()).ReturnsAsync(cartId);

            // Act
            Guid result = await _cartService.CreateAsync();

            // Assert
            Assert.Equal(cartId, result);
        }

        [Fact]
        public async Task RemoveFromCartAsync_WithInvalidCart_ThrowsNotFoundException()
        {
            // Arrange
            Guid cartId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            Cart cart = null;
            _cartRepositoryMock.Setup(repo => repo.GetAsync(cartId)).ReturnsAsync(cart);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _cartService.RemoveFromCartAsync(cartId, productId));
            _cartRepositoryMock.Verify(repo => repo.GetAsync(cartId), Times.Once);
            _cartRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Cart>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCartAsync_WithInvalidCart_ThrowsNotFoundException()
        {
            // Arrange
            Guid cartId = Guid.NewGuid();
            Guid productId = Guid.NewGuid();
            int quantity = 2;
            Cart cart = null;
            _cartRepositoryMock.Setup(repo => repo.GetAsync(cartId)).ReturnsAsync(cart);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _cartService.UpdateCartAsync(cartId, productId, quantity));
            _cartRepositoryMock.Verify(repo => repo.GetAsync(cartId), Times.Once);
            _cartRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Cart>()), Times.Never);
        }

        [Fact]
        public async Task CheckoutAsync_WithInvalidCart_ThrowsNotFoundException()
        {
            // Arrange
            Guid cartId = Guid.NewGuid();
            Cart cart = null;
            var checkoutDto = new CheckoutDTO();

            _cartRepositoryMock.Setup(repo => repo.GetAsync(cartId)).ReturnsAsync(cart);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _cartService.CheckoutAsync(cartId, checkoutDto));
            _cartRepositoryMock.Verify(repo => repo.GetAsync(cartId), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Order>()), Times.Never);
            _cartRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Cart>()), Times.Never);
        }
    }
}