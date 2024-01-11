
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface ILeaveBalanceRepository
    {
        Task<IEnumerable<LeaveBalanceDto>> GetLeaveBalanceAsync(int employeeId, CancellationToken cancellationToken);
    }
}
