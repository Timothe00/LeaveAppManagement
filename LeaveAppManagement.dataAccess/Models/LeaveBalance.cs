

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public int TotaLeaveAvailable { get; set; }
        public int TotalCurrentLeave { get; set; }
        public DateTime Years { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employees { get; set; }
        public int LeaveBalanceId { get; set; }
        public virtual LeaveBalance? LeaveBalances { get; set; }
    }
}
