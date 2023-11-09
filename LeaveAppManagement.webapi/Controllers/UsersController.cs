using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _iusersService;
        public UsersController(IUsersService iusersService)
        {
            _iusersService = iusersService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAllUserInTableAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _iusersService.GetUserServiceAsync(cancellationToken));
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
        [HttpPost]
        public async Task<IActionResult> PostUsersInTableAsync([FromBody] UsersDto usersDto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var user = await _iusersService.AddUsersServiceAsync(usersDto);
                return Ok(user);
            }

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInTableAsync([FromBody] UsersDto usersDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var user = await _iusersService.UpdateUserServiceAsync(usersDto, cancellationToken);
                return Ok(user);
            }
        }

        //DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInTableAsync(int id)
        {

            try
            {
                var del = await _iusersService.DeleteUserServiceAsync(id);
                if (del==null)
                {
                    return BadRequest("Cette donnnée n'existe pas !");    
                }
                return Ok(del);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
