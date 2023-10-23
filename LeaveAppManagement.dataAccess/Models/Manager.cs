

namespace LeaveAppManagement.dataAccess.Models
{
    public class Manager : Users
    {
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }
}
