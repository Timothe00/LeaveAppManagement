

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveReporting
    {
        public int Id { get; set; }
        // Statistiques sur l'utilisation des congés
        public int TotalRequest { get; set; } 
        public int TotalPending { get; set; }
        public int TotalApproved { get; set; } 
        public int TotalRejected { get; set; }
        public int TotalUsers { get; set; }

        public User? Users { get; set; }
        public int UserId { get; set; }
    }
}
