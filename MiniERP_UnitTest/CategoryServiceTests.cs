namespace MiniERP_UnitTest
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_DuplicateName_ThrowsException()
        {
            // Arrange
            var dto = new CategoryCreateUpdateDto { Name = "Laptop" };
            var existing = new ProductCategory { Name = "Laptop" };
            _mockRepo.Setup(r => r.GetByName("Laptop")).Returns(existing);

            // Act & Assert
            Action act = () => _categoryService.Create(dto);
            act.Should().Throw<Exception>().WithMessage("Tên danh mục 'Laptop' đã tồn tại.");
        }
    }
}
