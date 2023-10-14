

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveCalendar
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
