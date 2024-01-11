

namespace LeaveAppManagement.dataAccess.Models
{
    public class LeaveReporting
    {
        public int Id { get; set; }
        public int CurrentYear { get; set; }
        public int TotalRequest { get; set; } 
        public int TotalPending { get; set; }
        public int TotalApproved { get; set; } 
        public int TotalRejected { get; set; }
    }
}
