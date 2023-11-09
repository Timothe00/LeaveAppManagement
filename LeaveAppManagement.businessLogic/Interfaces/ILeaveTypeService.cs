using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<IEnumerable<LeaveTypeDto>> GetLeaveTypeServiceAsync(CancellationToken cancellationToken);
        Task<LeaveTypeDto> GetLeaveTypeByIdServiceAsync(int id, CancellationToken cancellationToken);
        Task<LeaveType> AddLeaveTypeServiceAsync(LeaveTypeDto leaveTypedto, CancellationToken cancellationToken);
        Task<LeaveType> UpdateLeaveTypeServiceAsync(LeaveTypeDto leaveTypedto, CancellationToken cancellationToken);
        Task<bool> DeleteLeaveRequestAsyncServiceAsync(int reqId, CancellationToken cancellationToken);
    }
}
