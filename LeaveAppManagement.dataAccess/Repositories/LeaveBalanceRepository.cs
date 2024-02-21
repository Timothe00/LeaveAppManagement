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

        public LeaveBalanceRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            //_leaveReportingRepository = leaveReportingRepository;
        }



        public async Task<IEnumerable<LeaveBalanceDto>> GetAllLeaveBalanceForAllEmployeeAsync(CancellationToken cancellationToken)
        {
            // Récupération des données de la base de données de manière asynchrone
            var leaveRequests = await _dbContext.LeaveRequests
                .Where(lr => lr.RequestStatus == "Accepted")
                .ToListAsync(cancellationToken);

            var users = await _dbContext.Users
                .Where(u => u.RoleId == 3) // Filtrez les utilisateurs par le rôle approprié
                .ToListAsync(cancellationToken);

            // Agrégation des informations pour chaque utilisateur
            var leaveBalanceDtos = new List<LeaveBalanceDto>();
            foreach (var user in users)
            {
                var acceptedLeaveDates = leaveRequests
                    .Where(lr => lr.EmployeeId == user.Id)
                    .SelectMany(lr => Enumerable.Range(0, (int)(lr.DateEnd - lr.DateStart).TotalDays)
                    .Select(offset => lr.DateStart.AddDays(offset)))
                    .ToList();

                // Calculer le total des jours acceptés
                int totalLeaveDays = acceptedLeaveDates.Count;
                // Obtenir l'année d'embauche
                var hireYear = user.HireDate;

                var monthsOfWork = (DateTime.Now - hireYear).Days/30;

                // Créer un objet LeaveBalanceDto pour chaque utilisateur
                var leaveBalanceDto = new LeaveBalanceDto
                {
                    EmployeeId = user.Id,
                    EmployeeName = $"{user.FirstName} {user.LastName}",
                    TotaLeaveAvailable = CalculateTotaLeaveAvailable(hireYear),
                    TotalLeaveUsed = totalLeaveDays,
                    TotalCurrentLeave = (monthsOfWork) - totalLeaveDays,
                };

                leaveBalanceDtos.Add(leaveBalanceDto);
            }

            return leaveBalanceDtos;
        }

        private static double CalculateTotaLeaveAvailable(DateTime hireDate)
        {
            var difference = (DateTime.Now - hireDate).Days / 30.0;

            double totaLeaveDaysAvailable = Math.Ceiling(difference) * 2.5; //2.5 est le nombre de jour de congé par mois par defaut selon l'entreprise
            return totaLeaveDaysAvailable;
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
                .Where(u => u.Id == employeeId && u.RoleId == 3)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                // Gérer le cas où l'utilisateur n'est pas trouvé
                return null; // Ou lancez une exception appropriée selon votre logique
            }

            // Calculer le total des jours acceptés
            int totalLeaveDays = acceptedLeaveDates.Count;

            // Obtenir l'année d'embauche
            var hireYear = user.HireDate;

            var monthsOfWork = (DateTime.Now - hireYear).Days / 30;

            // Créer un objet LeaveBalanceDto pour chaque utilisateur
            var leaveBalanceDto = new LeaveBalanceDto
            {
                EmployeeId = user.Id,
                EmployeeName = $"{user.FirstName} {user.LastName}",
                TotaLeaveAvailable = CalculateTotaLeaveAvailable(hireYear),
                TotalLeaveUsed = totalLeaveDays,
                TotalCurrentLeave = CalculateTotaLeaveAvailable(hireYear) - totalLeaveDays,
            };

            return leaveBalanceDto;
        }

    }
}
