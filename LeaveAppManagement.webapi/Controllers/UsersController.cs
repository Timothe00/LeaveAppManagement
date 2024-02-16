using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Interfaces.EmailModelService;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {

        private readonly LeaveAppManagementDbContext _dbContext;
        private readonly IUsersService _iusersService;
        private readonly IConfiguration _config;
        private readonly IEmailModelService _emailModelService;
        public UsersController(
            IUsersService iusersService, 
            LeaveAppManagementDbContext dbContext, 
            IConfiguration config,
            IEmailModelService emailModelService)
        {
            _iusersService = iusersService;
            _dbContext = dbContext;
            _config = config;
            _emailModelService = emailModelService;
        }
        // GET: api/<UsersController>
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUserInTableAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _iusersService.GetUserServiceAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUserInTableAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _iusersService.GetUserServiceByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //POST api/<UsersController>
        [HttpPost("add")]
        public async Task<IActionResult> PostUsersInTableAsync([FromBody] CreateUserDto usersDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var user = await _iusersService.AddUsersServiceAsync(usersDto);
                return Ok(new
                {
                    userCreate = user,
                    Message = "Utilisateur crée avec succès"
                });
            }

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInTableAsync([FromBody] UpdateUserDto usersDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var user = await _iusersService.UpdateUserServiceAsync(usersDto, cancellationToken);
                return Ok(new
                {
                    userUpdate = user,
                    Message = "Utilisateur modifié avec succès"
                });
            }
        }


        [HttpPut("password")]
        public async Task<IActionResult> PutUserPasswordInTableAsync([FromBody] UserPasswordUpdateDto userPasswordUpdateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var userPassword = await _iusersService.UserPasswordchangeServiceAsync(userPasswordUpdateDto, cancellationToken);
                return Ok(userPassword);
            }
        }

        //DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInTableAsync(int id)
        {

            try
            {
                var del = await _iusersService.DeleteUserServiceAsync(id);
                if (del == false)
                {
                    return BadRequest("Cette donnée n'existe pas !");
                }
                return Ok(new
                {
                    userDelete = del,
                    Message = "Utilisateur supprimé avec succès"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //POST api/<UsersController>
        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x=> x.Email == email);

            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "cette adresse email n'existe pas"
                });
            }

            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(10);
            string from = _config["EmailSettings:From"];
            var emailModel = new EmailModel(email, "réinitialiser le mot de passe", EmailBody.EmailStringBody(email, emailToken));
            _emailModelService.SendEmail(emailModel);
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Email envoyé"
            });
        }

        //POST api/<UsersController>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "cette adresse email n'existe pas"
                });
            }

            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "lien de réinitialisation invalide"
                });
            }
            user.Password = EncryptPassword.HashPswd(resetPasswordDto.NewPassword);
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "mot de passe réinitialisé avec succès"
            });
        }
    }
}
