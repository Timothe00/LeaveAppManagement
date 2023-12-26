using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _iLeaveRequestService;

        public LeaveRequestController(ILeaveRequestService iLeaveRequestService)
        {
            _iLeaveRequestService = iLeaveRequestService;
        }
        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequestInTableAsync(CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _iLeaveRequestService.GetLeaveRequestServiceAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneLeaveRequestInTableAsync(int id, CancellationToken cancellationToken)
        {
            var leave = await _iLeaveRequestService.GetLeaveRequestByIdServicAsync(id, cancellationToken);
            try
            {
                if (leave == null)
                {
                    return NotFound("Demande Non retrouvée");
                }
                return Ok(leave);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<IActionResult> PostLeaveRequestInTableAsync([FromBody] PosteLeaveRequestDto leaveRequestDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var req = await _iLeaveRequestService.AddLeaveRequestServiceAsync(leaveRequestDto, cancellationToken);
                return Ok(req);
            }
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveRequestInTableAsync([FromBody] UpdateLeaveRequestDto updateLeaveRequestDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var UpdateRequest = await _iLeaveRequestService.UpdateLeaveRequestServiceAsync(updateLeaveRequestDto, cancellationToken);
                return Ok(UpdateRequest);
            }
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("status")]
        public async Task<IActionResult> PutLeaveRequestSatusInTableAsync([FromBody] RequestStatusDto requestStatutDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var UpdateRequest = await _iLeaveRequestService.UpdateLeaveRequestStatusServiceAsync(requestStatutDto, cancellationToken);
                return Ok(UpdateRequest);
            }
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequestInTableAsync(int id)
        {
            try
            {
                var del = await _iLeaveRequestService.DeleteLeaveRequestAsyncServiceAsync(id);
                if (del == false)
                {
                    return BadRequest("Cette donnnée n'existe pas !");
                }
                return Ok(del);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
