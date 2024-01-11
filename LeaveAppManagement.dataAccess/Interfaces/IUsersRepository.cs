
using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task<User> UpdateUserPasswordAsync(User user, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> CheckEmailExistsAsync(string email);


    }
}
