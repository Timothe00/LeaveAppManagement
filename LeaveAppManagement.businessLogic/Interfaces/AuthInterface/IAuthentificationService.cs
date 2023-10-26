using LeaveAppManagement.dataAccess.Models.Authentification;
using LeaveAppManagement.dataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.businessLogic.Interfaces.AuthInterface
{
    public interface IAuthentificationService
    {
        Task<string> Authenticate(Login userLogin, CancellationToken cancellationToken);
    }
}
