using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersDto>> GetUserServiceAsync(CancellationToken cancellationToken);
        Task<UsersDto> GetUserServiceByIdAsync(int id, CancellationToken cancellationToken);
        Task<Users> AddUsersServiceAsync(UsersDto usersDto);
        Task<Users> UpdateUserServiceAsync(UsersDto usersDto);
        Task<bool> DeleteUserServiceAsync(int userId);
    }
}
