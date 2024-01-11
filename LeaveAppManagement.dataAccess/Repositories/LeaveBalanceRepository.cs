using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;
        //private readonly ILeaveReportingRepository _leaveReportingRepository;

        public LeaveBalanceRepository(LeaveAppManagementDbContext dbContext, ILeaveReportingRepository leaveReportingRepository)
        {
            _dbContext = dbContext;
            //_leaveReportingRepository = leaveReportingRepository;
        }


        public async Task<IEnumerable<LeaveBalanceDto>> GetLeaveBalanceAsync(int employeeId, CancellationToken cancellationToken)
        {
            // Récupérer les données de la base de données de manière asynchrone
            var leaveRequests = await _dbContext.LeaveRequests
                .Where(lr => lr.RequestStatus == "Accepted" && lr.EmployeeId == employeeId)
                .ToListAsync(cancellationToken);

            var acceptedLeaveDates = leaveRequests
                .SelectMany(lr => Enumerable.Range(0, (int)(lr.DateEnd - lr.DateStart).TotalDays)
                .Select(offset => lr.DateStart.AddDays(offset)))
                .ToList();  // Convertir en liste côté client

            var users = await _dbContext.Users
                .Where(user => user.RoleId == 3)
                .ToListAsync(cancellationToken);

            var leaveBalances = await _dbContext.LeaveBalances
                .ToListAsync(cancellationToken);

            // Calculer le total des jours acceptés
            int totalLeaveDays = acceptedLeaveDates.Count;

            var leaveBalanceQuery = from user in users
                                    join lr in leaveRequests on user.Id equals lr.EmployeeId
                                    select new LeaveBalanceDto
                                    {
                                        TotaLeaveAvailable = user.TotaLeaveAvailable,
                                        TotalLeaveUsed = totalLeaveDays,
                                        TotalCurrentLeave = user.TotaLeaveAvailable - totalLeaveDays,
                                        EmployeeName = user.FirstName +' ' + user.LastName,
                                        EmployeeId = user.Id,
                                    };

            return leaveBalanceQuery.ToList();
        }









    }
}
