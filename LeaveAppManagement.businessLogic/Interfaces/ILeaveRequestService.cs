using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestServiceAsync(CancellationToken cancellationToken);
        Task<LeaveRequestDto> GetLeaveRequestByIdServicAsync(int Id, CancellationToken cancellationToken);
        Task<LeaveRequest> UpdateLeaveRequestServiceAsync(UpdateLeaveRequestDto updateLeaveRequestDto, CancellationToken cancellationToken);
        Task<LeaveRequest> UpdateLeaveRequestStatusServiceAsync(RequestStatusDto RequestStatusDto, CancellationToken cancellationToken);
        Task<LeaveRequest> AddLeaveRequestServiceAsync(PosteLeaveRequestDto leaveRequest, CancellationToken cancellationToken);
        Task<bool> DeleteLeaveRequestAsyncServiceAsync(int reqId);
    }
}
