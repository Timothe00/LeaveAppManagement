using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersDto>> GetUserServiceAsync(CancellationToken cancellationToken);
        Task<UsersDto> GetUserServiceByIdAsync(int id, CancellationToken cancellationToken);
        Task<User?> AddUsersServiceAsync(CreateUserDto usersDto);
        Task<User?> UpdateUserServiceAsync(UpdateUserDto usersDto, CancellationToken cancellationToken);
        Task<User> UserPasswordchangeServiceAsync(UserPasswordUpdateDto userpasswordupdateDto, CancellationToken cancellationToken);
        Task<bool> DeleteUserServiceAsync(int userId);
    }
}
