

namespace LeaveAppManagement.dataAccess.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public List<LeaveRequest>? LeaveRequestPending { get; set; }
    }
}
