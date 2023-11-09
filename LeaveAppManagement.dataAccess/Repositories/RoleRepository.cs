using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;

        public RoleRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Role>> GetRoleAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Role> role = await _dbContext.Roles.ToListAsync();
            return role;
        }


        public async Task<Role> AddRoles(Role role, CancellationToken cancellationToken) 
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }





    }
}
