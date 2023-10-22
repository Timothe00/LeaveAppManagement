using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Une relation many-to-many avec les utilisateurs
        public ICollection<UserRole>? UserRoles { get; set; }

        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
    }
}
