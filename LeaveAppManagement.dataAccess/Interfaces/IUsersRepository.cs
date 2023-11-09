using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(int id);
        
    }
}
