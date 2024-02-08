

namespace LeaveAppManagement.dataAccess.Dto
{
    public class LeaveBalanceDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public double TotaLeaveAvailable { get; set; }
        public double TotalLeaveUsed { get; set; }
        public double TotalCurrentLeave { get; set; }
    }
}
