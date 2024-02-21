
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models.Authentification;
using LeaveAppManagement.dataAccess.Models;
using Moq;
using Microsoft.Extensions.Configuration;
using LeaveAppManagement.businessLogic.Services.AuthService;
using LeaveAppManagement.businessLogic.Interfaces.AuthInterface;
using LeaveAppManagement.dataAccess.Data;

namespace LeaveAppManagment.Test
{
    [TestClass]
    public class AuthentificationServiceTests
    {
        private Mock<IUsersRepository> _usersRepositoryMock;
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<IConfiguration> _configurationMock;
        private IAuthentificationService _authenticationService;
        private readonly LeaveAppManagementDbContext _DbContext;

        [TestInitialize]
        public void Initialize()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _configurationMock = new Mock<IConfiguration>();
            // Configurez vos mocks ici si nécessaire

            _authenticationService = new AuthentificationService(
                _usersRepositoryMock.Object,
                _roleRepositoryMock.Object,
                _configurationMock.Object,
                _DbContext
                );
        }

        [TestMethod]
        public async Task Authenticate_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userLogin = new Login { Email = "user@example.com", Password = "password" };
            var hashedPassword = EncryptPassword.HashPswd(userLogin.Password);
            //var roleName = "Admin"; // Supposons que RoleName est une propriété de type string

            _usersRepositoryMock.Setup(r => r.GetUsersAsync(It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new List<User> { });
            //_DbContext.Roles.Where(r => r.Id == user.RoleId).Select(role => role.RoleName)
            //                   .ReturnsAsync(new Role { RoleName = roleName });
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("your-secret-key");

            // Act
            var token = await _authenticationService.Authenticate(userLogin, CancellationToken.None);

            // Assert
            Assert.IsNotNull(token);
        }


        [TestMethod]
        public async Task Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var userLogin = new Login { Email = "user@example.com", Password = "wrong-password" };
            var user = new User { Id = 1, Email = "user@example.com", Password = EncryptPassword.HashPswd("password") };

            _usersRepositoryMock.Setup(r => r.GetUsersAsync(It.IsAny<CancellationToken>()))
                                .ReturnsAsync(new List<User> { user });

            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("your-secret-key");

            // Act
            var token = await _authenticationService.Authenticate(userLogin, CancellationToken.None);

            // Assert
            Assert.IsNull(token);
        }
    }
}
