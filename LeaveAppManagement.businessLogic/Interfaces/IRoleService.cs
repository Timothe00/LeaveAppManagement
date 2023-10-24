

using LeaveAppManagement.dataAccess.Dto;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRoleService(CancellationToken cancellationToken);
        Task<RoleDto> GetRoleByIdService(int id, CancellationToken cancellationToken);
    }
}
