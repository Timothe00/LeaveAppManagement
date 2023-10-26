using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync(CancellationToken cancellationToken);
        Task<Users> AddUserAsync(Users user);
        Task<Users> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(int id);
        
    }
}
