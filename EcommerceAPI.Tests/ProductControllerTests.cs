namespace EcommerceAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task Get_WithExistingProducts_ReturnsOkResult()
        {
            // Arrange
            var filter = new ProductFilterDTO();
            var products = new List<ProductDTO> { new ProductDTO { Id = Guid.NewGuid(), Name = "Product 1" }, new ProductDTO { Id = Guid.NewGuid(), Name = "Product 2" } };
            _productServiceMock.Setup(service => service.GetAsync(filter)).ReturnsAsync(products);

            // Act
            var result = await _productController.Get(filter);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(okResult.Value);
            Assert.Equal(products, returnedProducts);
        }

        [Fact]
        public async Task Get_WithNonexistentProducts_ReturnsNoContentResult()
        {
            // Arrange
            var filter = new ProductFilterDTO();
            _productServiceMock.Setup(service => service.GetAsync(filter)).ReturnsAsync((IEnumerable<ProductDTO>)null);

            // Act
            var result = await _productController.Get(filter);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetById_WithExistingProduct_ReturnsOkResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductDTO { Id = productId, Name = "Product 1" };
            _productServiceMock.Setup(service => service.GetAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productController.Get(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsAssignableFrom<ProductDTO>(okResult.Value);
            Assert.Equal(product, returnedProduct);
        }

        [Fact]
        public async Task GetById_WithNonexistentProduct_ReturnsNoContentResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productServiceMock.Setup(service => service.GetAsync(productId)).ReturnsAsync((ProductDTO)null);

            // Act
            var result = await _productController.Get(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CreateProduct_ValidProduct_ReturnsCreatedResult()
        {
            // Arrange
            var productDTO = new CreateProductDTO { Name = "Product 1", Price = 10.99m };
            var productId = Guid.NewGuid();
            _productServiceMock.Setup(service => service.CreateAsync(productDTO)).ReturnsAsync(productId);

            // Act
            var result = await _productController.CreateProduct(productDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            Assert.NotNull(createdResult.Value);
        }

        [Fact]
        public async Task AddProductToCart_ValidProduct_ReturnsNoContentResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var relatedProductId = Guid.NewGuid();

            // Act
            var result = await _productController.AddProductToCart(productId, relatedProductId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RemoveRelatedProduct_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var relatedProductId = Guid.NewGuid();

            // Act
            var result = await _productController.RemoveRelatedProduct(productId, relatedProductId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
