

using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface ILeaveReportingService
    {
        Task<LeaveReporting> GetAllLeaveStatisticsAsync(string role, CancellationToken cancellationToken);
        Task<LeaveReporting> GetUserLeaveStatisticsAsync(int userId, CancellationToken cancellationToken);
    }
}
