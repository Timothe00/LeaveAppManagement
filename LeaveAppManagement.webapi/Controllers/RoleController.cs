using LeaveAppManagement.businessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        // GET: api/<RoleController>
        [HttpGet]
        public async Task<IActionResult> GetAllRoleInTable(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _roleService.GetRoleService(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByIdInTable(int id, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleByIdService(id, cancellationToken);
            if(role == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(role);
            }
        }

        // POST api/<RoleController>
        //[HttpPost]
        //public async Task<IActionResult> PostRoleInTable([FromBody] RoleDto roleDto, CancellationToken cancellationToken)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data.");
        //    }
        //    else
        //    {
        //        var role = await _roleService.AddRoleServiceAsync(roleDto, cancellationToken);
        //        return Ok(role);
        //    }
        //}

        // PUT api/<RoleController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<RoleController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
