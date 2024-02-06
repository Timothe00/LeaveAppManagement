namespace LeaveAppManagement.dataAccess.Dto
{
    public class PosteLeaveRequestDto
    {
        public string DateRequest { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Commentary { get; set; } = string.Empty;
        public string? RequestStatus { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        
    }
}
