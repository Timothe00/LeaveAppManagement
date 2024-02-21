using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeDto>> GetLeaveTypeServiceAsync(CancellationToken cancellationToken);
        Task<LeaveTypeDto> GetLeaveTypeByIdServiceAsync(int id, CancellationToken cancellationToken);
        Task<LeaveType> AddLeaveTypeServiceAsync(LeaveTypeDto leaveTypedto, CancellationToken cancellationToken);
        Task<LeaveType> UpdateLeaveTypeServiceAsync(LeaveTypeDto leaveTypedto, CancellationToken cancellationToken);
        Task<bool> DeleteLeaveTypeAsyncServiceAsync(int leaveTypeId, CancellationToken cancellationToken);
    }
}
