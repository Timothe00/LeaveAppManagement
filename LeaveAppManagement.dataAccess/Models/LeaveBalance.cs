

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int Years { get; set; }
        public int EmployeeId { get; set; }
        public int TotalCurrentLeave { get; set; }
        public Employee? Employee { get; set; }
        public int LeaveRequestId { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
    }
}
