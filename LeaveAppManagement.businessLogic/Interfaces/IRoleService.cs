

using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRoleService(CancellationToken cancellationToken);
        Task<RoleDto> GetRoleByIdService(int id, CancellationToken cancellationToken);
        //Task<Role> AddRoleServiceAsync(RoleDto roleDto, CancellationToken cancellationToken);
    }
}
