namespace MiniERP_UnitTest
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void ChangePassword_WrongOldPassword_ThrowsException()
        {
            // Arrange
            var userId = 1;
            var dto = new UserPasswordChangeDto { OldPassword = "Wrong", NewPassword = "NewPassword123" };
            var user = new User { Id = userId, PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword") };
            
            _mockRepo.Setup(r => r.GetById(userId)).Returns(user);

            // Act & Assert
            Action act = () => _userService.ChangePassword(userId, dto);
            act.Should().Throw<Exception>().WithMessage("Mật khẩu cũ không chính xác.");
        }

        [Fact]
        public void ChangePassword_SameAsOld_ThrowsException()
        {
            // Arrange
            var userId = 1;
            var dto = new UserPasswordChangeDto { OldPassword = "CorrectPassword", NewPassword = "CorrectPassword" };
            var user = new User { Id = userId, PasswordHash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword") };
            
            _mockRepo.Setup(r => r.GetById(userId)).Returns(user);

            // Act & Assert
            Action act = () => _userService.ChangePassword(userId, dto);
            act.Should().Throw<Exception>().WithMessage("Mật khẩu mới không được trùng với mật khẩu cũ.");
        }
    }
}
