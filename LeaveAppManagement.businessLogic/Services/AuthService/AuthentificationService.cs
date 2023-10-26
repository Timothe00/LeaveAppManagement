using LeaveAppManagement.businessLogic.Interfaces.AuthInterface;
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Data;
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
        public readonly IRoleRepository _roleRepository;
        private readonly LeaveAppManagementDbContext _DbContext;

        public AuthentificationService(
            IUsersRepository usersRepository,
            IRoleRepository roleRepository,
            IConfiguration configuration,
            LeaveAppManagementDbContext DbContext
            )
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _DbContext = DbContext;
        }




        public async Task<string> Authenticate(Login userLogin, CancellationToken cancellationToken)
        {
           IEnumerable <Users> users = await _usersRepository.GetUsersAsync(cancellationToken);

            Users user = users.Where(u=> u.Email == userLogin.Email).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == EncryptPassword.HashPswd(userLogin.Password))
                {
                    return GenerateToken(user, cancellationToken);
                }
                return null;
            }
            
            return null;
        }


        public string GenerateToken(Users user, CancellationToken cancellationToken)
        {
            string? role = _DbContext.Roles.Where(r => r.Id == user.RoleId).Select(role => role.Name).FirstOrDefault();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.PrimarySid, Convert.ToString(user.Id) ),
                new Claim(ClaimTypes.Name, user.LastName),
                new Claim(ClaimTypes.Surname, user.FirstName),
                new Claim(ClaimTypes.Role ,role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
