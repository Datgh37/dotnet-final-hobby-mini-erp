namespace MiniERP_UnitTest
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _customerService = new CustomerService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_EmptyName_ThrowsException()
        {
            // Arrange
            var dto = new CustomerCreateUpdateDto { Name = "" };

            // Act & Assert
            Action act = () => _customerService.Create(dto);
            act.Should().Throw<Exception>().WithMessage("Tên khách hàng không được để trống.");
        }
    }
}
