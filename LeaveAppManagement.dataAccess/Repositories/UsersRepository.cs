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

        public async Task<IEnumerable<Users>> GetUsersAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Users> users = await _dbContext.Users.Include(u => u.Roles).ToListAsync();
            return users;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            _dbContext.Users.Include(u => u.Roles).ToListAsync();
            await _dbContext.SaveChangesAsync();
            return user;
        }


    }
}
