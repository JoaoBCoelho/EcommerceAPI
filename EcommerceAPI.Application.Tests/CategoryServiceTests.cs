namespace EcommerceAPI.Application.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsListOfCategoryDTO()
        {
            // Arrange
            List<Category> categories = new List<Category>
            {
                new Category (Guid.NewGuid(), "Category 1" ),
                new Category (Guid.NewGuid(), "Category 2" )
            };

            List<CategoryDTO> expectedCategoryDTOs = new List<CategoryDTO>
            {
                new CategoryDTO { Id = categories[0].Id, Name = categories[0].Name },
                new CategoryDTO { Id = categories[1].Id, Name = categories[1].Name }
            };
            _categoryRepositoryMock.Setup(repo => repo.GetAsync()).ReturnsAsync(categories);
            _mapperMock.Setup(mapper => mapper.Map<List<CategoryDTO>>(categories)).Returns(expectedCategoryDTOs);

            // Act
            List<CategoryDTO> result = await _categoryService.GetAsync();

            // Assert
            Assert.Equal(expectedCategoryDTOs, result);
        }
    }
}