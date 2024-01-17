using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;
        //private readonly ILeaveReportingRepository _leaveReportingRepository;

        public LeaveBalanceRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            //_leaveReportingRepository = leaveReportingRepository;
        }


        public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeeId, CancellationToken cancellationToken)
        {
            // Récupération des données de la base de données de manière asynchrone
            var leaveRequests = await _dbContext.LeaveRequests
                .Where(lr => lr.RequestStatus == "Accepted" && lr.EmployeeId == employeeId)
                .ToListAsync(cancellationToken);

            var acceptedLeaveDates = leaveRequests
                .SelectMany(lr => Enumerable.Range(0, (int)(lr.DateEnd - lr.DateStart).TotalDays)
                .Select(offset => lr.DateStart.AddDays(offset)))
                .ToList();  // Convertir en liste côté client

            var user = await _dbContext.Users
                .Where(user => user.Id == employeeId && user.RoleId == 3)
                .FirstOrDefaultAsync(cancellationToken);

            // Calculer le total des jours acceptés
            int totalLeaveDays = acceptedLeaveDates.Count;

            // Créer un seul objet LeaveBalanceDto
            var leaveBalanceDto = new LeaveBalanceDto
            {
                TotaLeaveAvailable = user?.TotaLeaveAvailable ?? 0,
                TotalLeaveUsed = totalLeaveDays,
                TotalCurrentLeave = user?.TotaLeaveAvailable - totalLeaveDays ?? 0,
                EmployeeName = user?.FirstName + ' ' + user?.LastName ?? "N/A",
                EmployeeId = employeeId
            };

            return leaveBalanceDto;
        }










    }
}
