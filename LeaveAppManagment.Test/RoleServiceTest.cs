

using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Moq;

namespace LeaveAppManagment.Test
{
    [TestClass]
    public class RoleServiceTest
    {
        private Mock<IRoleRepository> _roleRepositoryMock;
        private RoleService _roleService;

        [TestInitialize]
        public void Initialize()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _roleService = new RoleService(_roleRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetRoleService_ReturnsRoleDtos()
        {
            // Arrange
            var roles = new List<Role>
        {
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "Manager" },
        };

            _roleRepositoryMock.Setup(r => r.GetRoleAsync(CancellationToken.None)).ReturnsAsync(roles);

            // Act
            var result = await _roleService.GetRoleService(CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roles.Count(), result.Count());
        }

        [TestMethod]
        public async Task GetRoleByIdService_ReturnsRoleDto()
        {
            // Arrange
            var roleId = 1;
            var roles = new List<Role>
        {
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "Manager" },
            // Add more roles as needed
        };

            _roleRepositoryMock.Setup(r => r.GetRoleAsync(CancellationToken.None)).ReturnsAsync(roles);

            // Act
            var result = await _roleService.GetRoleByIdService(roleId, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roleId, result.Id);
            // Add more specific assertions if needed
        }
    }
}
