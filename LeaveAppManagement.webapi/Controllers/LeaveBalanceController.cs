using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalenceService _ileaveBalenceService;

        public LeaveBalanceController(ILeaveBalenceService ileaveBalenceService)
        {
            _ileaveBalenceService = ileaveBalenceService;
        }
        // GET: api/<LeaveBalanceController>
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveBalanceAsync(int employId, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _ileaveBalenceService.GetAllLeaveBalanceService(employId, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<LeaveBalanceController>/5
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployeeBalance(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _ileaveBalenceService.GetLeaveBalanceForEmployeesServiceAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
