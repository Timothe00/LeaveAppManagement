using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoleAsync(CancellationToken cancellationToken);
        Task<Role> AddRoles(Role role, CancellationToken cancellationToken);
        //Task<Role> GetRoleByName(Role ro, CancellationToken cancellationToken);
    }
}
