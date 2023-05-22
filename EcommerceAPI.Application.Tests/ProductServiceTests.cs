using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(
                _productRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task AddRelatedProductAsync_WithValidProducts_AddsRelatedProduct()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            var product = new Product("Test Product", 100, "https://example.com/image.jpg", 5, "Test Description", Guid.NewGuid(), null);
            var relatedProduct = new Product("Related Product", 200, "https://example.com/related-image.jpg", 3, "Related Description", Guid.NewGuid(), null);
            _productRepositoryMock.Setup(repo => repo.GetAsync(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.GetAsync(relatedProductId)).ReturnsAsync(relatedProduct);

            // Act
            await _productService.AddRelatedProductAsync(productId, relatedProductId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(product), Times.Once);
            Assert.Contains(relatedProductId, product.RelatedProducts.Select(pp => pp.RelatedProductId));
        }

        [Fact]
        public async Task AddRelatedProductAsync_WithInvalidProduct_ThrowsNotFoundException()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            Product product = null;
            var relatedProduct = new Product("Related Product", 200, "https://example.com/related-image.jpg", 3, "Related Description", Guid.NewGuid(), null);
            _productRepositoryMock.Setup(repo => repo.GetAsync(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.GetAsync(relatedProductId)).ReturnsAsync(relatedProduct);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.AddRelatedProductAsync(productId, relatedProductId));
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task AddRelatedProductAsync_WithInvalidRelatedProduct_ThrowsNotFoundException()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Guid relatedProductId = Guid.NewGuid();
            var product = new Product("Test Product", 100, "https://example.com/image.jpg", 5, "Test Description", Guid.NewGuid(), null);
            Product relatedProduct = null;
            _productRepositoryMock.Setup(repo => repo.GetAsync(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.GetAsync(relatedProductId)).ReturnsAsync(relatedProduct);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.AddRelatedProductAsync(productId, relatedProductId));
            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidRelatedProducts_ThrowsNotFoundException()
        {
            // Arrange
            var productDTO = new CreateProductDTO
            {
                Name = "Test Product",
                Price = 100,
                ImageUrl = "https://example.com/image.jpg",
                Description = "Test Description",
                Category = Guid.NewGuid(),
                RelatedProducts = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };
            var category = new Category(productDTO.Category, "Category");
            _categoryRepositoryMock.Setup(repo => repo.GetAsync(productDTO.Category)).ReturnsAsync(category);
            _productRepositoryMock.Setup(repo => repo.AllExistAsync(productDTO.RelatedProducts)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.CreateAsync(productDTO));
            _categoryRepositoryMock.Verify(repo => repo.GetAsync(productDTO.Category), Times.Never);
            _productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidCategory_ThrowsNotFoundException()
        {
            // Arrange
            var productDTO = new CreateProductDTO
            {
                Name = "Test Product",
                Price = 100,
                ImageUrl = "https://example.com/image.jpg",
                Description = "Test Description",
                Category = Guid.NewGuid()
            };
            Category category = null;
            _categoryRepositoryMock.Setup(repo => repo.GetAsync(productDTO.Category)).ReturnsAsync(category);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _productService.CreateAsync(productDTO));
            _categoryRepositoryMock.Verify(repo => repo.GetAsync(productDTO.Category), Times.Once);
            _productRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task GetAsync_WithValidProductId_ReturnsProduct()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            var product = new Product("Test Product", 100, "https://example.com/image.jpg", 5, "Test Description", Guid.NewGuid(), null);
            var productDTO = new ProductDTO { Id = productId };
            _productRepositoryMock.Setup(repo => repo.GetAsync(productId)).ReturnsAsync(product);
            _mapperMock.Setup(mapper => mapper.Map<ProductDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetAsync(productId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetAsync(productId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ProductDTO>(product), Times.Once);
            Assert.Equal(productDTO, result);
        }

        [Fact]
        public async Task GetAsync_WithInvalidProductId_ReturnsNull()
        {
            // Arrange
            Guid productId = Guid.NewGuid();
            Product product = null;
            _productRepositoryMock.Setup(repo => repo.GetAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetAsync(productId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetAsync(productId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ProductDTO>(It.IsAny<Product>()), Times.Once);
            Assert.Null(result);
        }
    }
}
