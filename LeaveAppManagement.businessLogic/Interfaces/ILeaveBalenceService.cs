using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface ILeaveBalenceService
    {
        Task<IEnumerable<LeaveBalanceDto>> GetLeaveBalanceForEmployeesServiceAsync(CancellationToken cancellationToken);
        Task<LeaveBalanceDto> GetAllLeaveBalanceService(int emplId, CancellationToken cancellationToken);
    }
}
