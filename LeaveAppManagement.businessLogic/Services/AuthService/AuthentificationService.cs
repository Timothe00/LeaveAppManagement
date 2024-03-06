using LeaveAppManagement.businessLogic.Interfaces.AuthInterface;
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using LeaveAppManagement.dataAccess.Models.Authentification;
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
        private readonly IRoleRepository _roleRepository;

        public AuthentificationService(
            IUsersRepository usersRepository,
            IRoleRepository roleRepository,
            IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
        }

        public async Task<string?> Authenticate(Login userLogin, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await _usersRepository.GetUsersAsync(cancellationToken);

            User? user = users.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == EncryptPassword.HashPswd(userLogin.Password));

            return await GenerateToken(user);
        }

        public async Task<string> GenerateToken(User user)
        {
            if (user == null)
            {
                return null;
            }

            Role roleName = await _roleRepository.GetRoleByIdAsync(user.RoleId, CancellationToken.None);

            if (roleName == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.PrimarySid, Convert.ToString(user.Id)),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, roleName?.RoleName) 
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }


}
