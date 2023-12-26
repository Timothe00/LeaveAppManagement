

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int DefaultTotaLeaveAvailable { get; set; } = 22;
        public int TotaLeaveAvailable { get; set; }
        public int TotalCurrentLeave { get; set; }
        public DateTime Years { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int LeaveRequestId { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
    }
}
