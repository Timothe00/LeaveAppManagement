﻿

namespace LeaveAppManagement.dataAccess.Dto
{
    public record ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string EmailToken { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
