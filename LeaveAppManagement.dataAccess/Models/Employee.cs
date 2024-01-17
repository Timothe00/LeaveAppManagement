

namespace LeaveAppManagement.dataAccess.Models
{
    // Classe Employe héritant de User
    public class Employee : User
    {
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }
}
