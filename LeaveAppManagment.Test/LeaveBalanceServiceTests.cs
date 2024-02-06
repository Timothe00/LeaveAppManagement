using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using Moq;

namespace LeaveAppManagment.Test
{
    [TestClass]
    public class LeaveBalanceServiceTests
    {
        private Mock<ILeaveBalanceRepository> _leaveBalanceRepositoryMock;
        private LeaveBalanceService _leaveBalanceService;

        [TestInitialize]
        public void Initialize()
        {
            _leaveBalanceRepositoryMock = new Mock<ILeaveBalanceRepository>();
            _leaveBalanceService = new LeaveBalanceService(_leaveBalanceRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetAllLeaveBalanceService_ReturnsLeaveBalanceDto()
        {
            // Arrange
            var employeeId = 1; // Replace with a valid employee ID
            var cancellationToken = CancellationToken.None;

            // Mock data for leave balance
            var leaveBalanceDto = new LeaveBalanceDto
            {
                EmployeeId = employeeId,
                EmployeeName = "Test",
                TotalCurrentLeave = 23,
                TotalLeaveUsed = 7,
                TotaLeaveAvailable = 30,
            };

            _leaveBalanceRepositoryMock.Setup(r => r.GetLeaveBalanceAsync(employeeId, cancellationToken)).ReturnsAsync(leaveBalanceDto);

            // Act
            var result = await _leaveBalanceService.GetAllLeaveBalanceService(employeeId, cancellationToken);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employeeId, result.EmployeeId);
        }
    }
}
