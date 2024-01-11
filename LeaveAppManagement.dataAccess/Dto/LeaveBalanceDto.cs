

namespace LeaveAppManagement.dataAccess.Dto
{
    public class LeaveBalanceDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int TotaLeaveAvailable { get; set; }
        public int TotalLeaveUsed { get; set; }
        public int TotalCurrentLeave { get; set; }
    }
}
