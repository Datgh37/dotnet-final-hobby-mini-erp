namespace MiniERP_UnitTest
{
    public class BrandServiceTests
    {
        private readonly Mock<IBrandRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BrandService _brandService;

        public BrandServiceTests()
        {
            _mockRepo = new Mock<IBrandRepository>();
            _mockMapper = new Mock<IMapper>();
            _brandService = new BrandService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_DuplicateName_ThrowsException()
        {
            // Arrange
            var dto = new BrandCreateUpdateDto { Name = "Sony" };
            var existing = new Brand { Name = "Sony" };
            _mockRepo.Setup(r => r.GetByName("Sony")).Returns(existing);

            // Act & Assert
            Action act = () => _brandService.Create(dto);
            act.Should().Throw<Exception>().WithMessage("Tên nhãn hàng 'Sony' đã tồn tại.");
        }

        [Fact]
        public void Create_EmptyName_ThrowsException()
        {
            // Arrange
            var dto = new BrandCreateUpdateDto { Name = "" };

            // Act & Assert
            Action act = () => _brandService.Create(dto);
            act.Should().Throw<Exception>().WithMessage("Tên nhãn hàng không được để trống.");
        }
    }
}
