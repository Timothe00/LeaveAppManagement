

namespace LeaveAppManagement.dataAccess.Dto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
       // public int TotaLeaveAvailable { get; set; }
        public DateTime HireDate { get; set; }
        public int RoleId { get; set; }
    }
}
