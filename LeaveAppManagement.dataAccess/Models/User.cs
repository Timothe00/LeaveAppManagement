

namespace LeaveAppManagement.dataAccess.Models
{
    public class User
    {
        // Classe de base pour les utilisateurs
        public int Id { get; set; }
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public int TotaLeaveAvailable { get; set; }
        public DateTime HireDate { get; set; }

        public string ResetPasswordToken { get; set; } = string.Empty;
        public DateTime ResetPasswordExpiry { get; set; }
        
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public ICollection<LeaveReporting>? LeaveReports { get; set; } 

        

    }
}
