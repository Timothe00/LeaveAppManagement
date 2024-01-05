using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface ILeaveReportingRepository
    {
        Task<int> GetTotalTotalLeaveRequestAsync(CancellationToken cancellationToken);
        Task<int> GetTotalPendingLeaveAsync(CancellationToken cancellationToken);
        Task<int> GetTotalAcceptedLeaveAsync(CancellationToken cancellationToken);
        Task<int> GetTotalRejectedLeaveAsync(CancellationToken cancellationToken);
    }
}
