using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;


namespace LeaveAppManagement.businessLogic.Interfaces.AuthInterface
{
    public interface IAuthentificationService
    {
        Task<string?> Authenticate(Login userLogin, CancellationToken cancellationToken);
        Task<string> GenerateToken(User user);
    }
}
