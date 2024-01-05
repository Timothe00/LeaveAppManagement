using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveReportingRepository : ILeaveReportingRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;

        public LeaveReportingRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        public async Task<int> GetTotalTotalLeaveRequestAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(cancellationToken);
        }


        public async Task<int> GetTotalPendingLeaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(l =>l.RequestStatus == "En attente", cancellationToken);
        }


        public async Task<int> GetTotalAcceptedLeaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(l => l.RequestStatus == "Accepted", cancellationToken);
        }


        public async Task<int> GetTotalRejectedLeaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(l => l.RequestStatus == "Rejected", cancellationToken);
        }


    }
}
