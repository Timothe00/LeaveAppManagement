using LeaveAppManagement.businessLogic.Interfaces.AuthInterface;
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeaveAppManagement.businessLogic.Services.AuthService
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;

        public AuthentificationService(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
        }



        public async Task<Users> Authenticate(Login userLogin)
        {
            var user = _usersRepository.Users.Where(u => u.Email == userLogin.Email).FirstOrDefault();
            if (user != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);
                if (isPasswordValid)
                {
                    return user;
                }
            }
            return null;
        }


        //public string GenerateToken(Users user)
        //{
        //    string? role = _DbContext.Roles.Where(r => r.Id == user.RoleId).Select(role => role.Name).FirstOrDefault();
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //                new Claim(ClaimTypes.PrimarySid, Convert.ToString(user.Id) ),
        //                new Claim(ClaimTypes.Name, user.LastName),
        //                new Claim(ClaimTypes.Surname, user.FirstName),
        //                new Claim(ClaimTypes.Role ,role)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

    }
}
