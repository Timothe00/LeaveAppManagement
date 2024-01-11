using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveReportingRepository : ILeaveReportingRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;

        private readonly IRoleRepository _roleRepository;

        public LeaveReportingRepository(LeaveAppManagementDbContext dbContext, IRoleRepository roleRepository)
        {
            _dbContext = dbContext;
            _roleRepository = roleRepository;
        }

        //Cette méthode retourner le nombre total de demandes de congé pour tous les utilisateurs
        public async Task<int> GetTotalTotalLeaveRequestAsync(string role, CancellationToken cancellationToken)
        {
            IEnumerable<Role> xrole = await _dbContext.Roles.Where(r => r.RoleName == role).ToListAsync();
            var total = await _dbContext.LeaveRequests.CountAsync(cancellationToken);

            if (role == "Admin" || role == "Manager")
            {
                // Si le rôle est "Admin", retourne le nombre total sans filtre
                return total;
            }
            else
            {
                // Si le rôle n'est pas "Admin", tu peux lever une exception
                throw new InvalidOperationException("La méthode GetTotalTotalLeaveRequestAsync ne s'applique pas aux rôles autres que Admin ou Manager.");
            }
        }


        //Cette méthode retourner le nombre total de demandes de congé pour un seul utilisateur
        public async Task<int> GetTotalTotalLeaveRequestForSingleUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests
                .Where(lt => lt.EmployeeId == userId)
                .CountAsync(cancellationToken);
        }

        //Cette méthode retourner le nombre total de demandes de congé en attente pour tous les utilisateurs
        public async Task<int> GetTotalPendingLeaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(l =>l.RequestStatus == "En attente", cancellationToken);
        }

        //Cette méthode retourne le nombre total de demandes de congé en attente pour un utilisateur spécifique
        public async Task<int> GetTotalPendingLeaveForSingleUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests
                .Where(lp => lp.EmployeeId == userId)
                .CountAsync(l => l.RequestStatus == "En attente", cancellationToken);
        }

        //Cette méthode retourne le nombre total de demandes de congé Acceptée pour tous les utilisateurs
        public async Task<int> GetTotalAcceptedLeaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(l => l.RequestStatus == "Accepted", cancellationToken);
        }

        //Cette méthode retourne le nombre total de demandes de congé Acceptée pour un utilisateur spécifique
        public async Task<int> GetTotalAcceptedLeaveForSingleUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests
                .Where(la => la.EmployeeId == userId)
                .CountAsync(l => l.RequestStatus == "Accepted", cancellationToken);
        }

        //Cette méthode retourne le nombre total de demandes de congé Rejetée pour tous les utilisateurs
        public async Task<int> GetTotalRejectedLeaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests.CountAsync(l => l.RequestStatus == "Rejected", cancellationToken);
        }

        //Cette méthode retourne le nombre total de demandes de congé rejetée pour un utilisateur spécifique
        public async Task<int> GetTotalRejectedLeaveForSingleUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.LeaveRequests
                .Where(lr => lr.EmployeeId == userId)
                .CountAsync(l => l.RequestStatus == "Rejected", cancellationToken);
        }


    }
}
