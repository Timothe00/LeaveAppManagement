

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveCalendar
    {
        public int Id { get; set; }
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
    }
}
