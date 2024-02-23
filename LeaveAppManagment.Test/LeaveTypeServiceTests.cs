using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Moq;


namespace LeaveAppManagment.Test
{
    [TestClass]
    public class LeaveTypeServiceTests
    {
        private Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
        private ILeaveTypeService _leaveTypeService;

        [TestInitialize]
        public void Initialize()
        {
            _leaveTypeRepositoryMock = new Mock<ILeaveTypeRepository>();
            _leaveTypeService = new LeaveTypeService(_leaveTypeRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetLeaveTypeServiceAsync_ReturnsLeaveTypes()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var leaveTypes = new List<LeaveType>
        {
            new LeaveType { Id = 1, LeaveTypeName = "Vacation" },
            new LeaveType { Id = 2, LeaveTypeName = "Sick Leave" },
        };

            // Configuration du mock pour simuler la récupération des types de congés
            _leaveTypeRepositoryMock.Setup(r => r.GetLeaveTypeAsync(cancellationToken)).ReturnsAsync(leaveTypes);

            // Act
            var result = await _leaveTypeService.GetLeaveTypeServiceAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(leaveTypes.Count, result.Count());
        }

        [TestMethod]
        public async Task GetLeaveTypeByIdServiceAsync_ReturnsSingleLeaveType()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var leaveTypeId = 1; 
            var leaveType = new LeaveType { Id = leaveTypeId, LeaveTypeName = "Vacation" };

            // Configuratio du mock pour simuler la récupération d'un type de congé unique
            _leaveTypeRepositoryMock.Setup(r => r.GetSingleLeaveTypeAsync(leaveTypeId, cancellationToken)).ReturnsAsync(leaveType);

            // Act
            var result = await _leaveTypeService.GetLeaveTypeByIdServiceAsync(leaveTypeId, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(leaveType.Id, result.Id);
            Assert.AreEqual(leaveType.LeaveTypeName, result.LeaveTypeName);
            
        }


        [TestMethod]
        public async Task AddLeaveTypeServiceAsync_ReturnsAddedLeaveType()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var leaveTypeDto = new LeaveTypeDto { Id = 1, LeaveTypeName = "Vacation" };

            // Configuration le mock pour simuler l'ajout d'un type de congé
            var addedLeaveType = new LeaveType { LeaveTypeName = "Vacation" };
            _leaveTypeRepositoryMock.Setup(r => r.AddLeaveTypeAsync(It.IsAny<LeaveType>(), cancellationToken)).ReturnsAsync(addedLeaveType);

            // Act
            var result = await _leaveTypeService.AddLeaveTypeServiceAsync(leaveTypeDto, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(addedLeaveType.Id, result.Id);
            Assert.AreEqual(addedLeaveType.LeaveTypeName, result.LeaveTypeName);
        }

        [TestMethod]
        public async Task UpdateLeaveTypeServiceAsync_ReturnsUpdatedLeaveType()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var leaveTypeDto = new LeaveTypeDto { Id = 1, LeaveTypeName = "Updated Vacation" };

            // Configurer le mock pour simuler la mise à jour d'un type de congé
            var updatedLeaveType = new LeaveType { Id = 1, LeaveTypeName = "Updated Vacation" };
            _leaveTypeRepositoryMock.Setup(r => r.UpdateLeaveTypeAsync(It.IsAny<LeaveType>(), cancellationToken)).ReturnsAsync(updatedLeaveType);

            // Act
            var result = await _leaveTypeService.UpdateLeaveTypeServiceAsync(leaveTypeDto, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedLeaveType.Id, result.Id);
            Assert.AreEqual(updatedLeaveType.LeaveTypeName, result.LeaveTypeName);
        }


        [TestMethod]
        public async Task DeleteLeaveTypeAsyncServiceAsync_InvalidLeaveTypeId_ReturnsFalse()
        {
            // Arrange
            int invalidLeaveTypeId = 0;

            // Act
            var result = await _leaveTypeService.DeleteLeaveTypeAsyncServiceAsync(invalidLeaveTypeId, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteLeaveTypeAsyncServiceAsync_ValidLeaveTypeId_DeletesAndReturnsTrue()
        {
            // Arrange
            int validLeaveTypeId = 1;

            // Configuration du mock pour simuler la suppression réussie
            _leaveTypeRepositoryMock.Setup(r => r.DeleteLeaveTypeAsync(validLeaveTypeId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _leaveTypeService.DeleteLeaveTypeAsyncServiceAsync(validLeaveTypeId, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            // Ajoutez des assertions supplémentaires si nécessaire pour vérifier le comportement du mock
        }


    }
}
