

using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Moq;

namespace LeaveAppManagment.Test
{
    [TestClass]
    public class LeaveRequestServiceTest
    {
        private Mock<ILeaveRequestRepository> _iLeaveRequestRepositoryMock;
        private ILeaveRequestService _ileaveRequestService;

        [TestInitialize]
        public void Initialize()
        {
            _iLeaveRequestRepositoryMock = new Mock<ILeaveRequestRepository>();
            _ileaveRequestService = new LeaveRequestService(_iLeaveRequestRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddLeaveRequestServiceAsync_CreateNewLeaveRequest_ReturnLeaveRequestIsValid()
        {
            // Arrange
            var newLeaveRequestDto = new PosteLeaveRequestDto
            {
                Commentary = "ok",
                EmployeeId = 2,
                LeaveTypeId = 2,
            };

            // Créer un objet LeaveRequest basé sur le PostLeaveRequestDto
            var expectedLeaveRequest = CreateLeaveRequest(newLeaveRequestDto);

            // Configurer le mock pour retourner l'objet LeaveRequest au lieu du PostLeaveRequestDto
            _iLeaveRequestRepositoryMock.Setup(r => r.AddLeaveRequestAsync(It.IsAny<LeaveRequest>())).ReturnsAsync(expectedLeaveRequest);

            // Act
            var result = await _ileaveRequestService.AddLeaveRequestServiceAsync(newLeaveRequestDto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLeaveRequest.Id, result.Id);
            Assert.AreEqual(expectedLeaveRequest.Commentary, result.Commentary);
            // Ajouter d'autres assertions au besoin
        }

        private LeaveRequest CreateLeaveRequest(PosteLeaveRequestDto leaveRequestDto)
        {
            // Créer et initialiser un objet LeaveRequest
            return new LeaveRequest
            {
                Commentary = leaveRequestDto.Commentary,
                EmployeeId = leaveRequestDto.EmployeeId,
                LeaveTypeId = leaveRequestDto.LeaveTypeId,
                // Initialiser d'autres propriétés si nécessaire
            };
        }



        [TestMethod]
        public async Task GetLeaveRequestByIdServiceAsync_ReturnsLeaveRequestDto()
        {
            // Arrange
            int leaveRequestId = 1;
            var cancellationToken = CancellationToken.None;

            // Créer un objet LeaveRequest simulé
            var leaveRequest = new LeaveRequestDto
            {
                Id = leaveRequestId,
                Commentary = "mariam joue à la balle",
                EmployeeId = 2,
                LeaveTypeName = "Mariage",
                // Initialiser d'autres propriétés si nécessaire
            };

            // Configuration du mock pour retourner l'objet LeaveRequest simulé
            _iLeaveRequestRepositoryMock.Setup(r => r.GetSingleLeaveRequestAsync(leaveRequestId, cancellationToken))
                                       .ReturnsAsync(leaveRequest);

            // Act
            var result = await _ileaveRequestService.GetLeaveRequestByIdServicAsync(leaveRequestId, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(leaveRequest.Id, result.Id);
            
        }


        [TestMethod]
        public async Task UpdateLeaveRequestServiceAsync_ReturnsUpdatedLeaveRequest()
        {
            // Arrange
            var updateLeaveRequestDto = new UpdateLeaveRequestDto
            {
                Id = 1,

                DateStart = DateTime.Now.AddDays(1),
                DateEnd = DateTime.Now.AddDays(5),
                Commentary = "commentaire mis à jour",
                LeaveTypeId = 2

            };

            var cancellationToken = CancellationToken.None;

            // Créer un objet LeaveRequest simulé avant la mise à jour
            var originalLeaveRequest = new LeaveRequestDto
            {
                Id = updateLeaveRequestDto.Id,

                DateStart = DateTime.Now.AddDays(-1),
                DateEnd = DateTime.Now.AddDays(3),
                Commentary = "commentaire original",
            };

            // Configurer le mock pour retourner l'objet LeaveRequest simulé avant la mise à jour
            _iLeaveRequestRepositoryMock.Setup(r => r.GetSingleLeaveRequestAsync(updateLeaveRequestDto.Id, cancellationToken))
                                       .ReturnsAsync(originalLeaveRequest);

            // Act
            var result = await _ileaveRequestService.UpdateLeaveRequestServiceAsync(updateLeaveRequestDto, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateLeaveRequestDto.Id, result.Id);
            Assert.AreEqual(updateLeaveRequestDto.DateRequest, result.DateRequest);
            Assert.AreEqual(updateLeaveRequestDto.DateStart, result.DateStart);
            Assert.AreEqual(updateLeaveRequestDto.DateEnd, result.DateEnd);
            Assert.AreEqual(updateLeaveRequestDto.Commentary, result.Commentary);
            Assert.AreEqual(updateLeaveRequestDto.LeaveTypeId, result.LeaveTypeId);
        }


        [TestMethod]
        public async Task UpdateLeaveRequestStatusServiceAsync_ReturnsUpdatedLeaveRequestStatus()
        {
            // Arrange
            var requestStatusDto = new RequestStatusDto
            {
                Id = 1,
                RequestStatus = "Acceptée"
                // Ajouter d'autres propriétés au besoin
            };

            var cancellationToken = CancellationToken.None;

            // Créer un objet LeaveRequest simulé avant la mise à jour du statut
            var originalLeaveRequest = new LeaveRequestDto
            {
                Id = requestStatusDto.Id,
                RequestStatus = "En attente"
            };

            // Configuration du mock pour retourner l'objet LeaveRequest simulé avant la mise à jour du statut
            _iLeaveRequestRepositoryMock.Setup(r => r.GetSingleLeaveRequestAsync(requestStatusDto.Id, cancellationToken))
                                       .ReturnsAsync(originalLeaveRequest);

            // Act
            var result = await _ileaveRequestService.UpdateLeaveRequestStatusServiceAsync(requestStatusDto, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(requestStatusDto.Id, result.Id);
            Assert.AreEqual(requestStatusDto.RequestStatus, result.RequestStatus);
        }

        [TestMethod]
        public async Task GetLeaveRequestServiceAsync_ReturnsAllLeaveRequests()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Créer une liste simulée d'objets LeaveRequestDto
            var leaveRequestDtos = new List<LeaveRequestDto>
        {
            new LeaveRequestDto { Id = 1, Commentary = "Request 1" },
            new LeaveRequestDto { Id = 2, Commentary = "Request 2" },
        };

            // Configuration du mock pour retourner la liste simulée d'objets LeaveRequestDto
            _iLeaveRequestRepositoryMock.Setup(r => r.GetLeaveRequestAsync(cancellationToken))
                                       .ReturnsAsync(leaveRequestDtos);

            // Act
            var result = await _ileaveRequestService.GetLeaveRequestServiceAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(leaveRequestDtos.Count(), result.Count());

        }


        [TestMethod]
        public async Task GetLeaveRequestByIdServiceAsync_ReturnsLeaveRequest()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var leaveRequestId = 1; // ID de la demande de congé à récupérer

            // Créer un objet LeaveRequestDto simulé
            var leaveRequestDto = new LeaveRequestDto
            {
                Id = leaveRequestId,
                Commentary = "Sample Leave Request",
                // Ajouter d'autres propriétés selon les besoins
            };

            // Configuration du mock pour retourner l'objet LeaveRequestDto simulé
            _iLeaveRequestRepositoryMock.Setup(r => r.GetSingleLeaveRequestAsync(leaveRequestId, cancellationToken))
                                       .ReturnsAsync(leaveRequestDto);

            // Act
            var result = await _ileaveRequestService.GetLeaveRequestByIdServicAsync(leaveRequestId, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(leaveRequestId, result.Id);

        }

        [TestMethod]
        public async Task DeleteLeaveRequestServiceAsync_ReturnsTrueForValidId()
        {
            // Arrange
            var reqId = 1; // ID de la demande de congé à supprimer

            // Configuration du mock pour simuler une suppression réussie
            _iLeaveRequestRepositoryMock.Setup(r => r.DeleteLeaveRequestAsync(reqId)).ReturnsAsync(true);

            // Act
            var result = await _ileaveRequestService.DeleteLeaveRequestAsyncServiceAsync(reqId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteLeaveRequestServiceAsync_ReturnsFalseForInvalidId()
        {
            // Arrange
            var reqId = -1; // ID invalide

            // Act
            var result = await _ileaveRequestService.DeleteLeaveRequestAsyncServiceAsync(reqId);

            // Assert
            Assert.IsFalse(result);
        }
    }

}

