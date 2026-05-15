namespace MiniERP_UnitTest
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockConfig = new Mock<IConfiguration>();

            // Mock JWT settings
            var jwtSection = new Mock<IConfigurationSection>();
            jwtSection.Setup(s => s["Key"]).Returns("MiniERP_Super_Secret_Key_2026_@Admin");
            jwtSection.Setup(s => s["Issuer"]).Returns("MiniERP_API");
            jwtSection.Setup(s => s["Audience"]).Returns("MiniERP_WebView");
            _mockConfig.Setup(c => c.GetSection("Jwt")).Returns(jwtSection.Object);

            _authService = new AuthService(_mockUserRepo.Object, _mockMapper.Object, _mockConfig.Object);
        }

        [Fact]
        public void Login_Success_ReturnsToken()
        {
            // Arrange
            var loginRequest = new LoginRequest { UserName = "admin", Password = "Admin@123" };
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            var user = new User { Id = 1, UserName = "admin", PasswordHash = hashedPassword, IsDeleted = false };
            var userDto = new UserDto { Id = 1, UserName = "admin", Role = "Admin" };
            
            _mockUserRepo.Setup(r => r.GetByUserName("admin")).Returns(user);
            _mockUserRepo.Setup(r => r.GetRoleName(1)).Returns("Admin");
            _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = _authService.Login(loginRequest);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeEmpty();
            result.User.UserName.Should().Be("admin");
        }

        [Fact]
        public void Login_WrongPassword_ThrowsException()
        {
            // Arrange
            var loginRequest = new LoginRequest { UserName = "admin", Password = "WrongPassword" };
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            var user = new User { Id = 1, UserName = "admin", PasswordHash = hashedPassword, IsDeleted = false };
            
            _mockUserRepo.Setup(r => r.GetByUserName("admin")).Returns(user);

            // Act & Assert
            Action act = () => _authService.Login(loginRequest);
            act.Should().Throw<Exception>().WithMessage("Sai tên đăng nhập hoặc mật khẩu.");
        }

        [Fact]
        public void Login_UserNotFound_ThrowsException()
        {
            // Arrange
            var loginRequest = new LoginRequest { UserName = "ghost", Password = "any" };
            _mockUserRepo.Setup(r => r.GetByUserName("ghost")).Returns((User?)null!);

            // Act & Assert
            Action act = () => _authService.Login(loginRequest);
            act.Should().Throw<Exception>().WithMessage("Sai tên đăng nhập hoặc mật khẩu.");
        }
    }
}
