using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Interfaces;
using Moq;

namespace LeaveAppManagment.Test
{
    [TestClass]
    public class LeaveReportingServiceTests
    {
        private Mock<ILeaveReportingRepository> _leaveReportingRepositoryMock;
        private LeaveReportingService _leaveReportingService;

        [TestInitialize]
        public void Initialize()
        {
            _leaveReportingRepositoryMock = new Mock<ILeaveReportingRepository>();
            _leaveReportingService = new LeaveReportingService(_leaveReportingRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllLeaveStatisticsAsync_ReturnsLeaveReporting()
        {
            // Arrange
            var role = "Admin"; // Replace with a valid role
            var cancellationToken = CancellationToken.None;

            // Données simulées pour le total des demandes de congés, en attente, approuvées et rejetées
            var totalLeaveRequests = 100;
            var totalPending = 20;
            var totalApproved = 60;
            var totalRejected = 20;

            _leaveReportingRepositoryMock.Setup(r => r.GetTotalTotalLeaveRequestAsync(role, cancellationToken)).ReturnsAsync(totalLeaveRequests);
            _leaveReportingRepositoryMock.Setup(r => r.GetTotalPendingLeaveAsync(cancellationToken)).ReturnsAsync(totalPending);
            _leaveReportingRepositoryMock.Setup(r => r.GetTotalAcceptedLeaveAsync(cancellationToken)).ReturnsAsync(totalApproved);
            _leaveReportingRepositoryMock.Setup(r => r.GetTotalRejectedLeaveAsync(cancellationToken)).ReturnsAsync(totalRejected);

            // Act
            var result = await _leaveReportingService.GetAllLeaveStatisticsAsync(role, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Now.Year, result.CurrentYear);
            Assert.AreEqual(totalLeaveRequests, result.TotalRequest);
            Assert.AreEqual(totalPending, result.TotalPending);
            Assert.AreEqual(totalApproved, result.TotalApproved);
            Assert.AreEqual(totalRejected, result.TotalRejected);
        }


        [TestMethod]
        public async Task GetUserLeaveStatisticsAsync_ReturnsLeaveReporting()
        {
            // Arrange
            var userId = 1; // Replace with a valid user ID
            var cancellationToken = CancellationToken.None;

            // Données simulées pour le nombre total de demandes de congés, en attente, approuvées et rejetées pour l'utilisateur
            var totalLeaveRequests = 50;
            var totalPending = 10;
            var totalApproved = 30;
            var totalRejected = 10;

            _leaveReportingRepositoryMock.Setup(r => r.GetTotalTotalLeaveRequestForSingleUserAsync(userId, cancellationToken)).ReturnsAsync(totalLeaveRequests);
            _leaveReportingRepositoryMock.Setup(r => r.GetTotalPendingLeaveForSingleUserAsync(userId, cancellationToken)).ReturnsAsync(totalPending);
            _leaveReportingRepositoryMock.Setup(r => r.GetTotalAcceptedLeaveForSingleUserAsync(userId, cancellationToken)).ReturnsAsync(totalApproved);
            _leaveReportingRepositoryMock.Setup(r => r.GetTotalRejectedLeaveForSingleUserAsync(userId, cancellationToken)).ReturnsAsync(totalRejected);

            // Act
            var result = await _leaveReportingService.GetUserLeaveStatisticsAsync(userId, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Now.Year, result.CurrentYear);
            Assert.AreEqual(totalLeaveRequests, result.TotalRequest);
            Assert.AreEqual(totalPending, result.TotalPending);
            Assert.AreEqual(totalApproved, result.TotalApproved);
            Assert.AreEqual(totalRejected, result.TotalRejected);
        }
    }
}
