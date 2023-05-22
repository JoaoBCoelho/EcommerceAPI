namespace EcommerceAPI.Domain.Tests
{
    public class CartTests
    {
        [Fact]
        public void CheckOut_ValidCart_SetIsCheckedOutToTrue()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());

            // Act
            cart.CheckOut();

            // Assert
            Assert.True(cart.IsCheckedOut);
        }

        [Fact]
        public void CheckOut_AlreadyCheckedOut_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            cart.CheckOut();

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.CheckOut());
        }

        [Fact]
        public void AddProduct_ValidProduct_AddsProductToCart()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            int quantity = 2;

            // Act
            cart.AddProduct(productId, quantity);

            // Assert
            Assert.Equal(1, cart.CartProducts.Count);
            Assert.Equal(productId, cart.CartProducts[0].ProductId);
            Assert.Equal(quantity, cart.CartProducts[0].Quantity);
        }

        [Fact]
        public void AddProduct_ProductAlreadyInCart_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            int quantity = 2;
            cart.AddProduct(productId, quantity);

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.AddProduct(productId, quantity));
        }

        [Fact]
        public void AddProduct_CartWasCheckedOut_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            int quantity = 2;
            cart.CheckOut();

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.AddProduct(productId, quantity));
        }

        [Fact]
        public void UpdateProductQuantity_ProductExists_UpdatesProductQuantity()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            int initialQuantity = 2;
            int updatedQuantity = 5;
            cart.AddProduct(productId, initialQuantity);

            // Act
            cart.UpdateProductQuantity(productId, updatedQuantity);

            // Assert
            Assert.Equal(updatedQuantity, cart.CartProducts[0].Quantity);
        }

        [Fact]
        public void AddProduct_InvalidQuantity_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            int initialQuantity = -2;

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.AddProduct(productId, initialQuantity));
        }

        [Fact]
        public void UpdateProductQuantity_ProductNotInCart_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            int quantity = 5;

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.UpdateProductQuantity(productId, quantity));
        }

        [Fact]
        public void RemoveProduct_ProductExists_RemovesProductFromCart()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();
            cart.AddProduct(productId, 2);

            // Act
            cart.RemoveProduct(productId);

            // Assert
            Assert.Equal(0, cart.CartProducts.Count);
        }

        [Fact]
        public void RemoveProduct_ProductNotInCart_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            Guid productId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.RemoveProduct(productId));
        }

        [Fact]
        public void ValidateCheckout_NotCheckedOut_NoExceptionThrown()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());

            // Act & Assert
            Assert.Null(Record.Exception(() => cart.ValidateCheckout()));
        }

        [Fact]
        public void ValidateCheckout_CheckedOut_ThrowDomainValidationException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            cart.CheckOut();

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => cart.ValidateCheckout());
        }
    }
}