namespace LeaveAppManagement.dataAccess.Dto
{
    public class PosteLeaveRequestDto
    {
        public DateTime DateRequest { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Commentary { get; set; } = string.Empty;
        public string? RequestStatus { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
