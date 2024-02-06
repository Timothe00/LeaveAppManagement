

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public string DateRequest { get; set; } = string.Empty;

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public string Commentary { get; set; } = string.Empty;
        public string? RequestStatus { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int LeaveTypeId { get; set; }
        public virtual LeaveType? LeaveType { get; set; }
    }
}
