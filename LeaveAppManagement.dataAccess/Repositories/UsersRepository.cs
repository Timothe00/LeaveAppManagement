using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
                    RoleId = u.RoleId,
                    Role = new Role
                    {
                       RoleName = r.RoleName
                    },
                    TotaLeaveAvailable = u.TotaLeaveAvailable,
                    PhoneNumber = u.PhoneNumber,
                    IsActiveUser = u.IsActiveUser,
                }
            ).ToListAsync(cancellationToken);

            return usersWithRoleNames;
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
                //users.Password = user.Password;
                users.Job = user.Job;
                users.TotaLeaveAvailable = user.TotaLeaveAvailable;
                users.PhoneNumber = user.PhoneNumber;
                users.RoleId = user.RoleId;
                users.IsActiveUser = user.IsActiveUser;
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
