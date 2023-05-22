namespace EcommerceAPI.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_ValidArguments_SetsProperties()
        {
            // Arrange
            string name = "Product Name";
            decimal price = 10.99m;
            string imageUrl = "http://example.com/image.jpg";
            int popularity = 3;
            string description = "Product description";
            Guid categoryId = Guid.NewGuid();
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            Product product = new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts);

            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
            Assert.Equal(imageUrl, product.ImageUrl);
            Assert.Equal(popularity, product.Popularity);
            Assert.Equal(description, product.Description);
            Assert.Equal(categoryId, product.CategoryId);
            Assert.Equal(relatedProducts.Count(), product.RelatedProducts.Count);
        }

        [Fact]
        public void Constructor_InvalidName_ThrowsDomainValidationException()
        {
            // Arrange
            string name = "";
            decimal price = 10.99m;
            string imageUrl = "http://example.com/image.jpg";
            int popularity = 3;
            string description = "Product description";
            Guid categoryId = Guid.NewGuid();
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<DomainValidationException>(() =>
                new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts));
        }

        [Fact]
        public void Constructor_InvalidPrice_ThrowsDomainValidationException()
        {
            // Arrange
            string name = "Product Name";
            decimal price = -10.99m;
            string imageUrl = "http://example.com/image.jpg";
            int popularity = 3;
            string description = "Product description";
            Guid categoryId = Guid.NewGuid();
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<DomainValidationException>(() =>
                new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts));
        }

        [Fact]
        public void Constructor_InvalidPopularity_ThrowsDomainValidationException()
        {
            // Arrange
            string name = "Product Name";
            decimal price = 10.99m;
            string imageUrl = "http://example.com/image.jpg";
            int popularity = 6;
            string description = "Product description";
            Guid categoryId = Guid.NewGuid();
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<DomainValidationException>(() =>
                new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts));
        }

        [Fact]
        public void Constructor_InvalidCategoryId_ThrowsDomainValidationException()
        {
            // Arrange
            string name = "Product Name";
            decimal price = 10.99m;
            string imageUrl = "http://example.com/image.jpg";
            int popularity = 3;
            string description = "Product description";
            Guid categoryId = Guid.Empty;
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<DomainValidationException>(() =>
                new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts));
        }

        [Fact]
        public void RemoveRelatedProduct_NonExistingRelatedProduct_ThrowsDomainValidationException()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            Product product = new Product(productId);
            product.AddRelatedProduct(relatedProductId);

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => product.RemoveRelatedProduct(Guid.NewGuid()));
        }

        [Fact]
        public void Constructor_InvalidImageUrl_ThrowsDomainValidationException()
        {
            // Arrange
            string name = "Product Name";
            decimal price = 10.99m;
            string imageUrl = "";
            int popularity = 3;
            string description = "Product description";
            Guid categoryId = Guid.NewGuid();
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<DomainValidationException>(() =>
                new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts));
        }

        [Fact]
        public void Constructor_InvalidRelatedProductId_ThrowsDomainValidationException()
        {
            // Arrange
            string name = "Product Name";
            decimal price = 10.99m;
            string imageUrl = "http://example.com/image.jpg";
            int popularity = 3;
            string description = "Product description";
            Guid categoryId = Guid.NewGuid();
            IEnumerable<Guid> relatedProducts = new List<Guid> { Guid.NewGuid(), Guid.Empty };

            // Act & Assert
            Assert.Throws<DomainValidationException>(() =>
                new Product(name, price, imageUrl, popularity, description, categoryId, relatedProducts));
        }

        [Fact]
        public void AddRelatedProduct_ValidRelatedProductId_AddsRelatedProduct()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            Product product = new Product(productId);

            // Act
            product.AddRelatedProduct(relatedProductId);

            // Assert
            Assert.Single(product.RelatedProducts);
            Assert.Equal(productId, product.RelatedProducts[0].ProductId);
            Assert.Equal(relatedProductId, product.RelatedProducts[0].RelatedProductId);
        }

        [Fact]
        public void AddRelatedProduct_RelatedProductAlreadyExists_ThrowsDomainValidationException()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            Product product = new Product(productId);
            product.AddRelatedProduct(relatedProductId);

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => product.AddRelatedProduct(relatedProductId));
        }

        [Fact]
        public void RemoveRelatedProduct_RelatedProductExists_RemovesRelatedProduct()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            Product product = new Product(productId);
            product.AddRelatedProduct(relatedProductId);

            // Act
            product.RemoveRelatedProduct(relatedProductId);

            // Assert
            Assert.Empty(product.RelatedProducts);
        }

        [Fact]
        public void RemoveRelatedProduct_RelatedProductNotInList_ThrowsDomainValidationException()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            Product product = new Product(productId);

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => product.RemoveRelatedProduct(relatedProductId));
        }
    }
}
