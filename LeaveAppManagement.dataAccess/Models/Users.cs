﻿

namespace LeaveAppManagement.dataAccess.Models
{
    public class Users
    {
        // Classe de base pour les utilisateurs
        public int Id { get; set; }
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<UserRole>? UserRoles { get; set; }

        public Users()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
    }
}
