using LeaveAppManagement.dataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync(CancellationToken cancellationToken);
        Task<Users> AddUserAsync(Users user);
    }
}
