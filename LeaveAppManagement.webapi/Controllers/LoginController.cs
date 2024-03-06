using LeaveAppManagement.businessLogic.Interfaces.AuthInterface;
using LeaveAppManagement.dataAccess.Models.Authentification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IAuthentificationService _iAuthentificationService;

        public LoginController(IAuthentificationService iAuthentificationService)
        {
            _iAuthentificationService = iAuthentificationService;
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login userLogin, CancellationToken cancellationToken)
        {
            string? token = await _iAuthentificationService.Authenticate(userLogin, cancellationToken);
            if (token != null)
            {
                return Ok(new
                {
                    Token = token,
                    Message = "Login Success"
                });
            }
            return NotFound("user not found");
        }

    }
}
