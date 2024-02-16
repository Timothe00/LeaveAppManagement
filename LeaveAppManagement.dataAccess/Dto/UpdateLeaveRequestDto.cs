namespace LeaveAppManagement.dataAccess.Dto
{
    public class UpdateLeaveRequestDto
    {
        public int Id { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Commentary { get; set; } = string.Empty;
        public int LeaveTypeId { get; set; }
    }
}
