
using LeaveAppManagement.businessLogic.Services.AuthService;
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;
using Microsoft.Extensions.Configuration;
using Moq;

namespace LeaveAppManagement.Tests.Services.AuthService
{
    [TestClass]
    public class AuthentificationServiceTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthentificationService _authService;

        public AuthentificationServiceTests()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _authService = new AuthentificationService(
                _usersRepositoryMock.Object,
                _roleRepositoryMock.Object,
                _configurationMock.Object);
        }

        [TestMethod]
        public async Task Authenticate_ShouldReturnToken_WhenValidCredentialsAreProvided()
        {
            // Arrange
            var userLogin = new Login { Email = "test@example.com", Password = "password" };
            var user = new User { Id = 1, Email = "test@example.com", Password = EncryptPassword.HashPswd("password"), RoleId = 1 };
            var role = new Role { RoleName = "User" };

            _usersRepositoryMock.Setup(repo => repo.GetUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User> { user });

            _roleRepositoryMock.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(role);

            _configurationMock.Setup(config => config["Jwt:Key"])
                .Returns("fm390IcmzNrvm9PlzqNe5EysVWcKZXUAppAe3fsvEFQ=");

            // Act
            var result = await _authService.Authenticate(userLogin, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        [Ignore]
        public async Task Authenticate_ShouldReturnNull_WhenInvalidCredentialsAreProvided()
        {
            // Arrange
            var userLogin = new Login { Email = "test@example.com", Password = "wrongpassword" };
            var user = new User { Id = 1, Email = "test@example.com", Password = EncryptPassword.HashPswd("password"), RoleId = 1 };

            _usersRepositoryMock.Setup(repo => repo.GetUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User> { user });

            _roleRepositoryMock.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Role)null); // Retourne un rôle null pour simuler le scénario où le rôle n'est pas trouvé.

            // Act
            var result = await _authService.Authenticate(userLogin, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }


    }
}
