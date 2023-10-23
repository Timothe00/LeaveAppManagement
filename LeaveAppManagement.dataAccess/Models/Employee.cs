

namespace LeaveAppManagement.dataAccess.Models
{
    // Classe Employe héritant de User
    public class Employee : Users
    {
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
        public ICollection<LeaveBalance>? LeaveBalances { get; set; }
    }
}
