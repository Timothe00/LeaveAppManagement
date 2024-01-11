
namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface ILeaveReportingRepository
    {
        Task<int> GetTotalTotalLeaveRequestAsync(string role, CancellationToken cancellationToken);
        Task<int> GetTotalPendingLeaveAsync(CancellationToken cancellationToken);
        Task<int> GetTotalAcceptedLeaveAsync(CancellationToken cancellationToken);
        Task<int> GetTotalRejectedLeaveAsync(CancellationToken cancellationToken);

        Task<int> GetTotalTotalLeaveRequestForSingleUserAsync(int userId, CancellationToken cancellationToken);
        Task<int> GetTotalPendingLeaveForSingleUserAsync(int userId, CancellationToken cancellationToken);
        Task<int> GetTotalAcceptedLeaveForSingleUserAsync(int userId, CancellationToken cancellationToken);
        Task<int> GetTotalRejectedLeaveForSingleUserAsync(int userId, CancellationToken cancellationToken);
    }
}
