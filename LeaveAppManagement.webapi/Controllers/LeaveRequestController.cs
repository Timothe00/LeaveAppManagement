using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Interfaces.EmailModelService;
using LeaveAppManagement.businessLogic.Services;
using LeaveAppManagement.businessLogic.Utility;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _iLeaveRequestService;
        private readonly IEmailModelService _emailModelService;
        private readonly IUsersService _usersService;

        public LeaveRequestController(ILeaveRequestService iLeaveRequestService, IEmailModelService emailModelService, IUsersService usersService)
        {
            _iLeaveRequestService = iLeaveRequestService;
            _emailModelService = emailModelService;
            _usersService = usersService;
        }
        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequestInTableAsync(CancellationToken cancellationToken)
        {

            try
            {
                var req = await _iLeaveRequestService.GetLeaveRequestServiceAsync(cancellationToken);

                return Ok(req);
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
                try
                {
                    var req = await _iLeaveRequestService.AddLeaveRequestServiceAsync(leaveRequestDto, cancellationToken);

                    // Appel de la méthode SendEmailToConfirm
                    var manager = await _usersService.GetSingleManagerServiceAsync(cancellationToken);
                    var emailModel = new EmailModel(manager.Email, "Demande de congé en attente de confirmation", EmailBody.EmailNotificationBody());
                    await _emailModelService.SendEmailToConfirm(emailModel, req.EmployeeId, cancellationToken);
                    return Ok(req);
                }
                catch (Exception ex)
                {
                    // Gère les exceptions de manière appropriée, enregistre ou renvoie une réponse d'erreur.
                    return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
                }
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
