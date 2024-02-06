
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface ILeaveBalanceRepository
    {
        Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeeId, CancellationToken cancellationToken);
        Task<IEnumerable<LeaveBalanceDto>> GetAllEmployeeLeaveBalanceAsync(CancellationToken cancellationToken);
    }
}
