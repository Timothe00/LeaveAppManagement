﻿

namespace LeaveAppManagement.dataAccess.Dto
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public double TotaLeaveAvailable { get; set; }
        public DateTime HireDate { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; } = null;
    }
}
