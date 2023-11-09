

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public uint DefaultTotaLeaveAvailable { get; set; } = 22;
        public uint TotaLeaveAvailable { get; set; }
        public uint TotalCurrentLeave { get; set; }
        public DateTime Years { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int LeaveRequestId { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
    }
}
