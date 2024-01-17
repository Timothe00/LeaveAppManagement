using LeaveAppManagement.businessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllRequestAcceptedController : ControllerBase
    {
        private readonly IAllRequestAcceptedService _iAllRequestAcceptedService;

        public AllRequestAcceptedController(IAllRequestAcceptedService iAllRequestAcceptedService)
        {
            _iAllRequestAcceptedService = iAllRequestAcceptedService;
        }


        // GET: api/<AllRequestAcceptedController>
        [HttpGet]
        public async Task<IActionResult> GetAllAcceptRequestAsync(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _iAllRequestAcceptedService.GetAllAcceptedReqAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AllRequestAcceptedController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

    }
}
