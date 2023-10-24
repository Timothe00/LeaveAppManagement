using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
