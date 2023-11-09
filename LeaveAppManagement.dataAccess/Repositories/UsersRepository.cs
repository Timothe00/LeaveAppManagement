using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<User> users = await _dbContext.Users.Include(u => u.Role).ToListAsync(cancellationToken);
            var usersWithRoleNames = users.Select(u => new User
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
                    RoleName = u.Role.RoleName
                },

                PhoneNumber = u.PhoneNumber,
                IsActiveUser = u.IsActiveUser,

            }).ToList();

            return usersWithRoleNames;
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
                users.Password = user.Password;
                users.Job = user.Job;
                users.PhoneNumber = user.PhoneNumber;
                users.RoleId = user.RoleId;
                users.IsActiveUser = user.IsActiveUser;
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
