

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int NumberOfDays { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Justification { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
