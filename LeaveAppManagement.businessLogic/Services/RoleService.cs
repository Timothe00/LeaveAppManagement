using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Repositories;

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
                Name = r.Name,
            });
            return roledto;
        }

        public async Task<RoleDto> GetRoleByIdService(int id, CancellationToken cancellationToken)
        {
            IEnumerable<Role> role = await _iroleRepository.GetRoleAsync(cancellationToken);
            var oneRole = role.Where(us => us.Id == id).FirstOrDefault();
            RoleDto roleDto = new RoleDto
            {
                Id= oneRole.Id,
                Name = oneRole.Name,
            };
            return roleDto;
        }

    }
}
