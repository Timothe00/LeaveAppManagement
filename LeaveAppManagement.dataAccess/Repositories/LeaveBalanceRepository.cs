using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;

        public LeaveBalanceRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<IEnumerable<LeaveBalance>> GetLeaveBalanceAsync(CancellationToken cancellationToken)
        //{
        //    var leaveRequests = await _dbContext.LeaveRequests.ToListAsync(cancellationToken);
        //    var users = await _dbContext.Users.Where(user => user.RoleId == 3).ToListAsync(cancellationToken);
        //    var leaveBalances = await _dbContext.LeaveBalances.ToListAsync(cancellationToken);

        //    uint defaultTotaLeaveAvailable = 22; // La valeur par défaut

        //    var leaveBalanceQuery = from leave in leaveRequests
        //                            join user in users on leave.EmployeeId equals user.Id
        //                            join leaveBalance in leaveBalances on leave.LeaveTypeId equals leaveBalance.Id
        //                            select new LeaveBalance
        //                            {
        //                                TotaLeaveAvailable = defaultTotaLeaveAvailable - leave.NumberOfDays,
        //                                TotalCurrentLeave = leaveBalance.TotaLeaveAvailable - leave.NumberOfDays,
        //                                Years = leaveBalance.Years,
        //                                EmployeeId = user.Id,
        //                                LeaveRequestId = leave.Id
        //                            };

        //    return leaveBalanceQuery.ToList();
        //}



    }
}
