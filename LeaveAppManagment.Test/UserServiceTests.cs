
using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Moq;
using System.Threading;

namespace LeaveAppManagment.Test
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUsersRepository> _usersRepositoryMock;
        private IUsersService _usersService;

        [TestInitialize]

        public void Initialize()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _usersService = new UsersService(_usersRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddUsersServiceAsync_CreateNewUser_returnUserIsValid()
        {
            // Arrange
            var newUserDto = new CreateUserDto { RoleId = 1 };

            // Créez un objet User basé sur le CreateUserDto
            var expectedUser = CreateUser(newUserDto);

            //renvoyer l'objet User au lieu du CreateUserDto
            _usersRepositoryMock.Setup(r => r.AddUserAsync(It.IsAny<User>())).ReturnsAsync(expectedUser);

            // Act
            var result = await _usersService.AddUsersServiceAsync(newUserDto);

            // Assert
            Assert.AreEqual(expectedUser.FirstName, result.FirstName);
        }

        private User CreateUser(CreateUserDto userDto)
        {
            switch (userDto.RoleId)
            {
                case 1:
                    return new Admin
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        
                    };
                case 2:
                    return new Manager
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        
                    };
                case 3:
                    return new Employee
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        
                    };
                default:
                    return null; 
            }
        }

        [TestMethod]
        public async Task UpdateUserServiceAsync_UpdateNewUser_returnUserIsValid()
        {
            // Arrange
            var userId = 1; // ID de l'utilisateur à mettre à jour
            var userDto = new UpdateUserDto { RoleId = 1, Id = userId, FirstName = "John", LastName = "Doe" };

            // Créez un objet User basé sur le UpdateUserDto
            var expectedUser = UpdateUser(userDto);

            // Configurez le mock pour renvoyer l'objet User au lieu du UpdateUserDto
            _usersRepositoryMock.Setup(r => r.UpdateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                               .Callback<User, CancellationToken>((user, cancellationToken) =>
                               {
                                   // Mise à jour des propriétés d'utilisateur avec celles de UpdateUserDto
                                   user.Id = userDto.Id;
                                   user.FirstName = userDto.FirstName;
                                   user.LastName = userDto.LastName;
                                   // ... autres propriétés ...
                               })
                               .ReturnsAsync(expectedUser);

            // Act
            var result = await _usersService.UpdateUserServiceAsync(userDto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.FirstName, result.FirstName);
            Assert.AreEqual(expectedUser.LastName, result.LastName);
            // Ajoutez d'autres assertions au besoin
        }

        private User UpdateUser(UpdateUserDto userDto)
        {
            switch (userDto.RoleId)
            {
                case 1:
                    return new Admin
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                    };
                case 2:
                    return new Manager
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                    };
                case 3:
                    return new Employee
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                    };
                default:
                    return null;
            }
        }

        [TestMethod]
        public async Task GetUserServiceAsync_ReturnsListOfUsersDto()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Créez une liste d'utilisateurs fictifs
            var users = new List<User>
    {
        new Employee
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "hashedpassword",
            PhoneNumber = "123-456-7890",
            Job = "Software Developer",
            TotaLeaveAvailable = 20,
            RoleId = 3,
            Role = new Role { RoleName = "Employee" }
        },
        new Manager
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "hashedpassword",
            PhoneNumber = "987-654-3210",
            Job = "Project Manager",
            TotaLeaveAvailable = 25,
            RoleId = 2,
            Role = new Role { RoleName = "Manager" }
        }
    };

            //retourner la liste fictive d'utilisateurs
            _usersRepositoryMock.Setup(r => r.GetUsersAsync(cancellationToken)).ReturnsAsync(users);

            // Act
            var result = await _usersService.GetUserServiceAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UsersDto>));

            // Vérifiez si la liste contient les utilisateurs attendus
            var resultList = result.ToList();

            Assert.AreEqual(users.Count, resultList.Count);

            for (int i = 0; i < users.Count; i++)
            {
                var expectedUser = users[i];
                var actualUser = resultList[i];

                Assert.AreEqual(expectedUser.Id, actualUser.Id);
                Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
                Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
                Assert.AreEqual(expectedUser.Email, actualUser.Email);
            }
        }


        [TestMethod]
        public async Task GetUserServiceByIdAsync_ReturnsUserDtoById()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            int userIdToRetrieve = 1; // ID de l'utilisateur à récupérer

            // Créez une liste d'utilisateurs fictifs
            var users = new List<User>
    {
        new Employee
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "hashedpassword",
            PhoneNumber = "123-456-7890",
            Job = "Software Developer",
            TotaLeaveAvailable = 20,
            RoleId = 3,
            Role = new Role { RoleName = "Employee" }
        },
        new Manager
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "hashedpassword",
            PhoneNumber = "987-654-3210",
            Job = "Project Manager",
            TotaLeaveAvailable = 25,
            RoleId = 2,
            Role = new Role { RoleName = "Manager" }
        }

    };

            //retourner la liste fictive d'utilisateurs
            _usersRepositoryMock.Setup(r => r.GetUsersAsync(cancellationToken)).ReturnsAsync(users);

            // Act
            var result = await _usersService.GetUserServiceByIdAsync(userIdToRetrieve, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UsersDto));

            // Recherchez l'utilisateur correspondant dans la liste fictive
            var expectedUser = users.FirstOrDefault(u => u.Id == userIdToRetrieve);

            Assert.IsNotNull(expectedUser);

            // Vérifiez si les propriétés de UsersDto correspondent à celles de l'utilisateur attendu
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.FirstName, result.FirstName);
            Assert.AreEqual(expectedUser.LastName, result.LastName);
            Assert.AreEqual(expectedUser.Email, result.Email);
        }

        [TestMethod]
        public async Task DeleteUserServiceAsync_ValidUserId_DeletesUserSuccessfully()
        {
            // Arrange
            int userIdToDelete = 1; // ID de l'utilisateur à supprimer

            // retourner une valeur true lors de l'appel à DeleteUserAsync
            _usersRepositoryMock.Setup(r => r.DeleteUserAsync(userIdToDelete)).ReturnsAsync(true);

            // Act
            var result = await _usersService.DeleteUserServiceAsync(userIdToDelete);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task GetManagersServiceAsync_ReturnsManagersDto()
        {
            // Arrange

            var cancellationToken = CancellationToken.None;

            // Création d'une liste fictive de managers
            var managers = new List<User>
        {

            new Manager
            {
                Id = 1,
                FirstName = "Manager1",
                LastName = "Manager1LastName",
                Email = "manager1@example.com",
                RoleId = 2,
                // Autres propriétés
            },
            new Manager
            {
                Id = 2,
                FirstName = "Manager2",
                LastName = "Manager2LastName",
                Email = "manager2@example.com",
                RoleId = 2,
                // Autres propriétés
            }
        };

            // Configuration du mock pour renvoyer la liste fictive de managers
            _usersRepositoryMock.Setup(r => r.GetUsersByRoleIdAsync(2, cancellationToken)).ReturnsAsync(managers);

            // Act
            var result = await _usersService.GetManagersServiceAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UsersDto>));

            // Vérifions si la liste contient les managers attendus
            var resultList = result.ToList();

            Assert.AreEqual(managers.Count, resultList.Count);

            for (int i = 0; i < managers.Count; i++)
            {
                var expectedManager = managers[i];
                var actualManager = resultList[i];

                Assert.AreEqual(expectedManager.Id, actualManager.Id);
                Assert.AreEqual(expectedManager.FirstName, actualManager.FirstName);
                Assert.AreEqual(expectedManager.LastName, actualManager.LastName);
                Assert.AreEqual(expectedManager.Email, actualManager.Email);
            }
        }


        [TestMethod]
        public async Task GetSingleManagerServiceAsync_ReturnsSingleManagerDto()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Créez un manager fictif
            var manager = new Manager
            {
                Id = 1,
                FirstName = "Manager1",
                LastName = "Manager1LastName",
                Email = "manager1@example.com",
                RoleId = 2,
            };

            // Configuration du mock pour renvoyer le manager fictif
            _usersRepositoryMock.Setup(r => r.GetSingleManagerByRoleIdAsync(2, cancellationToken)).ReturnsAsync(manager);

            // Act
            var result = await _usersService.GetSingleManagerServiceAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UsersDto));

            // Vérifions si les propriétés du manager correspondent à celles de UsersDto
            Assert.AreEqual(manager.Id, result.Id);
            Assert.AreEqual(manager.FirstName, result.FirstName);
            Assert.AreEqual(manager.LastName, result.LastName);
            Assert.AreEqual(manager.Email, result.Email);
        }

        [TestMethod]
        public async Task GetSingleManagerServiceAsync_NoManagerFound_ReturnsNull()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Configuration du mock pour renvoyer null (aucun manager trouvé)
            _usersRepositoryMock.Setup(r => r.GetSingleManagerByRoleIdAsync(2, cancellationToken)).ReturnsAsync((Manager)null);

            // Act
            var result = await _usersService.GetSingleManagerServiceAsync(cancellationToken);

            // Assert
            Assert.IsNull(result);
        }


    }
}
