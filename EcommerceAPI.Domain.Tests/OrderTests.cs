namespace EcommerceAPI.Domain.Tests
{
    public class OrderTests
    {
        private readonly BillingInformation validBillingInformation = new BillingInformation("John Doe", "123 Street", "City", "State", "12345", "Country");
        private readonly ShippingInformation validShippingInformation = new ShippingInformation("John Doe", "123 Street", "City", "State", "12345", "Country");
        private readonly string validCustomerEmail = "test@example.com";
        private static Guid cartId = Guid.NewGuid();
        private static Guid productId = Guid.NewGuid();
        private readonly Cart validCart = new Cart(cartId, new List<CartProduct>() { new CartProduct(cartId, productId, 3) });

        [Fact]
        public void Constructor_ValidArguments_SetsProperties()
        {
            // Act
            Order order = new Order(validBillingInformation, validShippingInformation, validCustomerEmail, validCart);

            // Assert
            Assert.Equal(validBillingInformation, order.BillingInformation);
            Assert.Equal(validShippingInformation, order.ShippingInformation);
            Assert.Equal(validCustomerEmail, order.CustomerEmail);
            Assert.Equal(validCart.Id, order.CartId);
        }

        [Fact]
        public void Constructor_InvalidCart_ThrowNotFoundException()
        {
            // Arrange
            Cart cart = null;

            // Act & Assert
            Assert.Throws<NotFoundException>(() => new Order(validBillingInformation, validShippingInformation, validCustomerEmail, cart));
        }

        [Fact]
        public void Constructor_CheckedOutCart_ThrowDomainValidationException()
        {
            // Arrange
            var invalidCart = new Cart(cartId, new List<CartProduct>() { new CartProduct(cartId, productId, 3) });
            invalidCart.CheckOut();

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => new Order(validBillingInformation, validShippingInformation, validCustomerEmail, invalidCart));
        }

        [Fact]
        public void Constructor_CartWithoutProducts_ThrowNotFoundException()
        {
            // Arrange
            Cart cart = new Cart(Guid.NewGuid(), new List<CartProduct>());
            cart.CheckOut();

            // Act & Assert
            Assert.Throws<NotFoundException>(() => new Order(validBillingInformation, validShippingInformation, validCustomerEmail, cart));
        }

        [Fact]
        public void Constructor_InvalidBillingInformation_ThrowDomainValidationException()
        {
            // Arrange
            BillingInformation billingInformation = null;

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => new Order(billingInformation, validShippingInformation, validCustomerEmail, validCart));
        }

        [Fact]
        public void Constructor_InvalidShippingInformation_ThrowDomainValidationException()
        {
            // Arrange
            ShippingInformation shippingInformation = null;

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => new Order(validBillingInformation, shippingInformation, validCustomerEmail, validCart));
        }

        [Fact]
        public void Constructor_InvalidCustomerEmail_ThrowDomainValidationException()
        {
            // Arrange
            string customerEmail = "";

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => new Order(validBillingInformation, validShippingInformation, customerEmail, validCart));
        }
    }
}
