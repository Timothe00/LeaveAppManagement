

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string? LeaveTypeName { get; set; }

        public virtual ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }
}
