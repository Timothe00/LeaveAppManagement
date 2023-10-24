using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoleAsync(CancellationToken cancellationToken);
    }
}
