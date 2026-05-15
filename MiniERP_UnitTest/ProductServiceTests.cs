namespace MiniERP_UnitTest
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _productService = new ProductService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_DuplicateSKU_ThrowsException()
        {
            // Arrange
            var dto = new ProductCreateUpdateDto { SKU = "DUPE-01", Name = "Test" };
            var existingProduct = new Product { SKU = "DUPE-01" };
            
            _mockRepo.Setup(r => r.GetBySku("DUPE-01")).Returns(existingProduct);

            // Act & Assert
            Action act = () => _productService.CreateProduct(dto);
            act.Should().Throw<Exception>().WithMessage("Mã SKU 'DUPE-01' đã tồn tại.");
        }

        [Fact]
        public void Create_Success_ReturnsNewId()
        {
            // Arrange
            var dto = new ProductCreateUpdateDto { SKU = "NEW-01", Name = "Test" };
            var productEntity = new Product { SKU = "NEW-01", Name = "Test" };
            
            _mockRepo.Setup(r => r.GetBySku("NEW-01")).Returns((Product?)null!);
            _mockMapper.Setup(m => m.Map<Product>(dto)).Returns(productEntity);
            _mockRepo.Setup(r => r.Add(productEntity)).Returns(100);

            // Act
            var result = _productService.CreateProduct(dto);

            // Assert
            result.Should().Be(100);
            _mockRepo.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Delete_SoftDelete_CallsRepository()
        {
            // Arrange
            int productId = 1;

            // Act
            _productService.DeleteProduct(productId);

            // Assert
            _mockRepo.Verify(r => r.Delete(productId), Times.Once);
        }
    }
}
