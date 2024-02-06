
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface ILeaveBalenceService
    {
        Task<IEnumerable<LeaveBalanceDto>> GetLeaveBalanceServiceForEmployeeAsync(CancellationToken cancellationToken);
        Task<LeaveBalanceDto> GetAllLeaveBalanceService(int emplId, CancellationToken cancellationToken);
    }
}
