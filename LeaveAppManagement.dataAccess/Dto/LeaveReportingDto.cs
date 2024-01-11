

namespace LeaveAppManagement.dataAccess.Dto
{
    public class LeaveReportingDto
    {

        public int TotalRequest { get; set; }
        public int TotalPending { get; set; }
        public int TotalApproved { get; set; }
        public int TotalRejected { get; set; }
    }
}
