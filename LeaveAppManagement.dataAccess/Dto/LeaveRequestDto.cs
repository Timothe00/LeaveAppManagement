namespace LeaveAppManagement.dataAccess.Dto
{
    public class LeaveRequestDto
    {
        public int Id { get; set; }
        public string DateRequest { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Commentary { get; set; } = string.Empty;
        public string? RequestStatus { get; set; }
        public string? LeaveTypeName { get; set; }
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

}