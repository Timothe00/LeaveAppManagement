

namespace LeaveAppManagement.dataAccess.Models
{
    public class Manager : User
    {
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }
}
