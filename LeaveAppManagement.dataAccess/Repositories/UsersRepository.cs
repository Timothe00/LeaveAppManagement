using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;
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

        public async Task<IEnumerable<Users>> GetUsersAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Users> users = await _dbContext.Users.Include(u => u.Roles).ToListAsync();
            return users;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            _ = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Users> UpdateUserAsync(Users user)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser != null)
            {
                _dbContext.Users.Update(existingUser);
                await _dbContext.SaveChangesAsync();
                return existingUser;
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
