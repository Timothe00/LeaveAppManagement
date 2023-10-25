using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Models.Authentification
{
    public class Login
    {
        [Required(ErrorMessage = "L'addresse Email est requis")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le Mot de passe est requis")]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
