

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LeaveType>? LeaveTypes { get; set; }
    }
}
