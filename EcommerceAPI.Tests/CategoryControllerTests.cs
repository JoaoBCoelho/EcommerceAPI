namespace EcommerceAPI.Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoryController _categoryController;

        public CategoryControllerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Get_WithCategories_ReturnsOkResult()
        {
            // Arrange
            var categories = new List<CategoryDTO> { new CategoryDTO { Id = Guid.NewGuid(), Name = "Category 1" } };
            _categoryServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCategories = Assert.IsAssignableFrom<List<CategoryDTO>>(okResult.Value);
            Assert.Equal(categories, returnedCategories);
        }

        [Fact]
        public async Task Get_WithNoCategories_ReturnsNoContentResult()
        {
            // Arrange
            var categories = new List<CategoryDTO>();
            _categoryServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryController.Get();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}
