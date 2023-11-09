using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _iLeaveTypeService;
        public LeaveTypeController(ILeaveTypeService iLeaveTypeService)
        {
            _iLeaveTypeService = iLeaveTypeService;
        }

        // GET: api/<LeaveTypeController>
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveTypeInTable(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _iLeaveTypeService.GetLeaveTypeServiceAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<LeaveTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveTypeByIdInTableAsync(int id, CancellationToken cancellationToken)
        {
            var singleLeaveType = await _iLeaveTypeService.GetLeaveTypeByIdServiceAsync(id, cancellationToken);
            if (singleLeaveType == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(singleLeaveType);
            }
        }

        // POST api/<LeaveTypeController>
        [HttpPost]
        public async Task<IActionResult> PostLeaveTypeInTableAsync([FromBody] LeaveTypeDto leaveTypeDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var newLeaveType = await _iLeaveTypeService.AddLeaveTypeServiceAsync(leaveTypeDto, cancellationToken);
                return Ok(newLeaveType);
            }
        }

        // PUT api/<LeaveTypeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveTypeInTableAsync([FromBody] LeaveTypeDto leaveTypeDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var updateLeaveType = await _iLeaveTypeService.UpdateLeaveTypeServiceAsync(leaveTypeDto, cancellationToken);
                return Ok(updateLeaveType);
            }
        }

        // DELETE api/<LeaveTypeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveTypeInTableAsync(int  leaveTypeId, CancellationToken cancellationToken)
        {
            try
            {
                var leaveTypeToDelete = await _iLeaveTypeService.DeleteLeaveRequestAsyncServiceAsync(leaveTypeId, cancellationToken);
                if (leaveTypeToDelete == false)
                {
                    return BadRequest("Cette donnnée n'existe pas !");
                }
                return Ok(leaveTypeToDelete);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
