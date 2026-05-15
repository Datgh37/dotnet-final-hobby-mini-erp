namespace MiniERP_UnitTest
{
    public class SalesOrderServiceTests
    {
        private readonly Mock<ISalesOrderRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SalesOrderService _salesOrderService;

        public SalesOrderServiceTests()
        {
            _mockRepo = new Mock<ISalesOrderRepository>();
            _mockMapper = new Mock<IMapper>();
            _salesOrderService = new SalesOrderService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void PlaceOrder_EmptyItems_ThrowsException()
        {
            // Arrange
            var dto = new CreateSalesOrderDto { Items = new List<CreateSalesOrderItemDto>() };

            // Act & Assert
            Action act = () => _salesOrderService.PlaceOrder(dto);
            act.Should().Throw<Exception>().WithMessage("Đơn hàng phải có ít nhất một sản phẩm.");
        }

        [Fact]
        public void PlaceOrder_Success_ReturnsOrderId()
        {
            // Arrange
            var dto = new CreateSalesOrderDto 
            { 
                CustomerId = 1,
                Items = new List<CreateSalesOrderItemDto> 
                { 
                    new CreateSalesOrderItemDto { ProductId = 1, Quantity = 2, UnitPrice = 100 } 
                } 
            };
            var entity = new SalesOrder();
            
            _mockMapper.Setup(m => m.Map<SalesOrder>(dto)).Returns(entity);
            _mockRepo.Setup(r => r.CreateOrder(entity)).Returns(500);

            // Act
            var result = _salesOrderService.PlaceOrder(dto);

            // Assert
            result.Should().Be(500);
            entity.TotalAmount.Should().Be(200);
            entity.OrderNumber.Should().StartWith("SO-");
        }
    }
}
