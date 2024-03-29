﻿using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveAppManagement.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportToExcelController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveBalenceService _leaveBalenceService;
        private readonly IExportExcelService _exportExcelService;

        public ExportToExcelController(ILeaveRequestService leaveRequestService, IExportExcelService exportExcelService, ILeaveBalenceService leaveBalenceService)
        {
            _leaveRequestService = leaveRequestService;
            _exportExcelService = exportExcelService;
            _leaveBalenceService = leaveBalenceService;
        }

        // GET: api/<ExportToExcelController>
        [HttpGet]
        public async Task<IActionResult> ExportToExcel( CancellationToken cancellationToken)
        {
            var leaveRequestDtos = await _leaveRequestService.GetLeaveRequestServiceAsync(cancellationToken); // la méthode GetLeaveRequestServiceAsync() pour récupérer toutes les demandes de congé

            var leaveBalance = await _leaveBalenceService.GetLeaveBalanceForEmployeesServiceAsync(cancellationToken);

            var fileName = "Rapport"; // nom du fichier

            // Utilisez la méthode GenerateExcel de votre service
            return _exportExcelService.GenerateExcel(fileName, leaveRequestDtos, leaveBalance);
        }
    }
}
