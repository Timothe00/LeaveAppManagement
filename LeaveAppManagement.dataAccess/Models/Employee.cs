

namespace LeaveAppManagement.dataAccess.Models
{
    // Classe Employe héritant de User
    public class Employee : Users
    {
        public List<LeaveRequest>? LeaveRequests { get; set; }
       public List<LeaveBalance>? LeaveBalances { get; set; }
    }
}
