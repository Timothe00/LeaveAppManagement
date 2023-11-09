using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _iroleRepository;
        public RoleService(IRoleRepository iroleRepository)
        {
            _iroleRepository = iroleRepository;
        }

        public async Task<IEnumerable<RoleDto>> GetRoleService(CancellationToken cancellationToken)
        {
            IEnumerable<Role> role = await _iroleRepository.GetRoleAsync(cancellationToken);

            IEnumerable<RoleDto> roledto = role.Select(r => new RoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName,
            });
            return roledto;
        }

        public async Task<RoleDto> GetRoleByIdService(int id, CancellationToken cancellationToken)
        {
            IEnumerable<Role> role = await _iroleRepository.GetRoleAsync(cancellationToken);
            var oneRole = role.Where(us => us.Id == id).FirstOrDefault();
            RoleDto roleDto = new RoleDto
            {
                Id = oneRole.Id,
                RoleName = oneRole.RoleName,
            };
            return roleDto;
        }

    }
}
