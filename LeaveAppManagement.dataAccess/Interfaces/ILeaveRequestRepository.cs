using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestAsync(CancellationToken cancellationToken);
        Task<LeaveRequestDto> GetSingleLeaveRequestAsync(int Id, CancellationToken cancellationToken);
        Task<LeaveRequest> UpdateLeaveRequestAsync(UpdateLeaveRequestDto updatReq, CancellationToken cancellation);
        Task<LeaveRequest> UpdateRequestStatus(RequestStatusDto requestStatusDto, CancellationToken cancellationToken);
        Task<LeaveRequest> AddLeaveRequestAsync(LeaveRequest leaveRequest);
        Task<bool> DeleteLeaveRequestAsync(int id);
    }
}
