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

                // Obtenir le mois actuel
                var currentMonth = DateTime.Now.Month;

                // Obtenir le mois d'embauche
                var hireMonth = user.HireDate.Month;

                // Obtenir l'année d'embauche
                var hireYear = user.HireDate.Year;

                // Ajuster le calcul en fonction de l'année d'embauche
                var monthsOfWork = (DateTime.Now.Year - hireYear) * 12;

                // Créer un objet LeaveBalanceDto pour chaque utilisateur
                var leaveBalanceDto = new LeaveBalanceDto
                {
                    EmployeeId = user.Id,
                    EmployeeName = $"{user.FirstName} {user.LastName}",
                    TotaLeaveAvailable = monthsOfWork * 2.5,
                    TotalLeaveUsed = totalLeaveDays,
                    TotalCurrentLeave = (monthsOfWork * 2.5) - totalLeaveDays,
                };

                leaveBalanceDtos.Add(leaveBalanceDto);
            }

            return leaveBalanceDtos;
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

            // Obtenir le mois actuel
            var currentMonth = DateTime.Now.Month;

            // Obtenir le mois d'embauche
            var hireMonth = user.HireDate.Month;

            // Obtenir l'année d'embauche
            var hireYear = user.HireDate.Year;

            // Ajuster le calcul en fonction de l'année d'embauche
            var monthsOfWork = (DateTime.Now.Year - hireYear) * 12;

            // Créer un seul objet LeaveBalanceDto
            var leaveBalanceDto = new LeaveBalanceDto
            {
                TotaLeaveAvailable = monthsOfWork * 2.5,
                TotalLeaveUsed = totalLeaveDays,
                TotalCurrentLeave = (monthsOfWork * 2.5) - totalLeaveDays,
                EmployeeName = $"{user.FirstName} {user.LastName}",
                EmployeeId = employeeId
            };

            return leaveBalanceDto;
        }











    }
}
