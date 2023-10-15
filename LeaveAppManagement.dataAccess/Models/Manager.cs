

namespace LeaveAppManagement.dataAccess.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public ICollection<LeaveRequest>? LeaveRequestPending { get; set; }
    }
}
