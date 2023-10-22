
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.Identity.Client;

namespace LeaveAppManagement.businessLogic.Services.UsersCase
{
    public class UsersService : IUsersService
    {
        private readonly LeaveAppManagementDbContext _dbContext;
        public UsersService(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            List<Users> users = _dbContext.TUsers.Select(u => new Users()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserRoles = (ICollection<UserRole>)u.UserRoles.Select(x => x.Role.Name).ToList(),
            }).ToList();

            return await Task.FromResult(users);
        }

    }
}
