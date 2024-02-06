using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using Moq;


namespace LeaveAppManagment.Test
{
    [TestClass]
    public class AllRequestAcceptedServiceTests
    {

        private Mock<IAllRequestsAcceptedRepository> _allRequestsAcceptedRepositoryMock;
        private IAllRequestAcceptedService _allRequestsAcceptedService;


        [TestInitialize]
        public void Initialize()
        {
            _allRequestsAcceptedRepositoryMock = new Mock<IAllRequestsAcceptedRepository>();
            _allRequestsAcceptedService = new AllRequestAcceptedService(_allRequestsAcceptedRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllAcceptedReqAsync_ReturnsListOfAllRequestAcceptedDto()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Mock data for the list of accepted requests
            var acceptedRequests = new List<AllRequestAcceptedDto>
        {
            new AllRequestAcceptedDto { Title = "John Doe" },
            new AllRequestAcceptedDto { Title = "Jane david" },
            // Add more sample data as needed
        };

            _allRequestsAcceptedRepositoryMock.Setup(r => r.GetAllRequestsAccepted(cancellationToken)).ReturnsAsync(acceptedRequests);

            // Act
            var result = await _allRequestsAcceptedService.GetAllAcceptedReqAsync(cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(acceptedRequests.ToList(), result.ToList());
        }
    }
}
