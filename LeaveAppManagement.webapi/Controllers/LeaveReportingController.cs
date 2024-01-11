using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveReportingController : ControllerBase
    {
        private readonly ILeaveReportingService _iLeaveReportingService;
        public LeaveReportingController(ILeaveReportingService iLeaveReportingService)
        {
            _iLeaveReportingService = iLeaveReportingService;
        }


        // GET: api/<LeaveReportingController>
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequestAsync(string role, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _iLeaveReportingService.GetAllLeaveStatisticsAsync(role, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<LeaveReportingController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveRequestForUserAsync(int id, CancellationToken cancellationToken)
        {
            var stats = await _iLeaveReportingService.GetUserLeaveStatisticsAsync(id, cancellationToken);
            try
            {
                if (stats == null)
                {
                    return NotFound("Demande Non retrouvée");
                }
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
