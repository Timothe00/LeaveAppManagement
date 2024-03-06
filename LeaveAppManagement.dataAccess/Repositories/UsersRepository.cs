using DocumentFormat.OpenXml.Spreadsheet;
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace LeaveAppManagement.dataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;
        public UsersRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var currentMonth = DateTime.Now;

            var usersWithRoleNames = await (
                from u in _dbContext.Users
                join r in _dbContext.Roles on u.RoleId equals r.Id
                select new User
                {
                    Id = u.Id,
                    LastName = u.LastName,
                    FirstName = u.FirstName,
                    Email = u.Email,
                    Password = u.Password,
                    Job = u.Job,
                    HireDate = u.HireDate,
                    RoleId = u.RoleId,
                    Role = new Role
                    {
                        RoleName = r.RoleName
                    },
                    TotaLeaveAvailable = CalculateTotaLeaveAvailable(u.HireDate, currentMonth),
                    PhoneNumber = u.PhoneNumber,
                }
            ).ToListAsync(cancellationToken);

            return usersWithRoleNames;
        }

        //obtenir les utilisateurs en fonction de leur role
        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.Where(u => u.RoleId == roleId).ToListAsync(cancellationToken);
        }

        public async Task<User?> GetSingleManagerByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.RoleId == roleId, cancellationToken);
        }


        // Méthode pour calculer le TotaLeaveAvailable en fonction de la durée de service
        private static double CalculateTotaLeaveAvailable(DateTime hireDate, DateTime currentMonth)
        {
            // Calcul de la différence entre les deux dates
            TimeSpan difference = currentMonth - hireDate;

            // Obtention du nombre total de mois (approximation)
            int totalMonths = (int)(difference.TotalDays / 30); // Environ 30 jours par mois

            // Calcul du TotaLeaveAvailable en fonction de la durée de service
            double totaLeaveAvailable = Math.Max(totalMonths * 2.5, 0); // Assure que le résultat est positif

            return totaLeaveAvailable;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var userMail = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return userMail != null;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _ = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync(cancellationToken);
            if (users != null)
            {
                users.Id = user.Id;
                users.LastName = user.LastName;
                users.FirstName = user.FirstName;
                users.Email = user.Email;
                users.HireDate = user.HireDate;
                users.Job = user.Job;
                users.PhoneNumber = user.PhoneNumber;
                users.RoleId = user.RoleId;
                _dbContext.Users.Update(users);
                await _dbContext.SaveChangesAsync();
            }
            return null;

        }

        public async Task<User> UpdateUserPasswordAsync(User user, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync(cancellationToken);
            if (users != null)
            {
                users.Id = user.Id;
                users.Password = user.Password;
                _dbContext.Users.Update(users);
                await _dbContext.SaveChangesAsync();
            }
            return null;

        }



        public async Task<bool> DeleteUserAsync(int id)
        {
            var userDelete = await _dbContext.Users.FindAsync(id);

            if (userDelete != null)
            {
                _dbContext.Users.Remove(userDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
