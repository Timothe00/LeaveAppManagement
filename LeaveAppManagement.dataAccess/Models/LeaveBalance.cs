

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int TotaLeaveAvailable { get; set; }
        public int TotalCurrentLeave { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
