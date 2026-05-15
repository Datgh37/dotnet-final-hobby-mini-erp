namespace MiniERP_UnitTest
{
    public class SupplierServiceTests
    {
        private readonly Mock<ISupplierRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SupplierService _supplierService;

        public SupplierServiceTests()
        {
            _mockRepo = new Mock<ISupplierRepository>();
            _mockMapper = new Mock<IMapper>();
            _supplierService = new SupplierService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_DuplicateName_ThrowsException()
        {
            // Arrange
            var dto = new SupplierCreateUpdateDto { Name = "Bandai" };
            var existing = new Supplier { Name = "Bandai" };
            _mockRepo.Setup(r => r.GetByName("Bandai")).Returns(existing);

            // Act & Assert
            Action act = () => _supplierService.Create(dto);
            act.Should().Throw<Exception>().WithMessage("Nhà cung cấp 'Bandai' đã tồn tại.");
        }
    }
}
