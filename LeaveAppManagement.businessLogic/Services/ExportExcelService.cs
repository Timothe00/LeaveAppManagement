

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LeaveAppManagement.businessLogic.Services
{
    public class ExportExcelService: IExportExcelService
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveReportingService _leaveReportingService;

        public ExportExcelService(ILeaveRequestService leaveRequestService, ILeaveReportingService leaveReportingService)
        {
            _leaveRequestService = leaveRequestService;
            _leaveReportingService = leaveReportingService;  
        }

        public FileResult GenerateExcel(string fileName, IEnumerable<LeaveRequestDto> leaveRequestDtos, IEnumerable<LeaveBalanceDto> leaveBalanceDtos)
        {
            using (XLWorkbook xLWorkbook = new XLWorkbook())
            {
                // Première feuille pour LeaveRequestDto
                DataTable leaveRequestTable = CreateDataTableForLeaveRequest();
                foreach (var leave in leaveRequestDtos)
                {
                    leaveRequestTable.Rows
                        .Add(
                        leave.FirstName,
                        leave.LastName,
                        leave.DateRequest,
                        leave.NumberOfDays,
                        leave.DateStart,
                        leave.DateEnd,
                        leave.Commentary,
                        leave.RequestStatus,
                        leave.LeaveTypeName);
                }
                xLWorkbook.Worksheets.Add(leaveRequestTable, "Demandes de congé");

                // Deuxième feuille pour LeaveBalanceDto
                DataTable leaveBalanceTable = CreateDataTableForLeaveBalance();
                foreach (var balance in leaveBalanceDtos)
                {
                    leaveBalanceTable.Rows
                        .Add(
                        balance.EmployeeName,
                        balance.TotaLeaveAvailable,
                        balance.TotalLeaveUsed,
                        balance.TotalCurrentLeave);
                }
                xLWorkbook.Worksheets.Add(leaveBalanceTable, "Solde Congé");

                using (MemoryStream stream = new MemoryStream())
                {
                    xLWorkbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
                }
            }
        }

        private DataTable CreateDataTableForLeaveRequest()
        {
            DataTable dataTable = new DataTable("LeaveRequests");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nom"),
                new DataColumn("Prénoms"),
                new DataColumn("Jour de la demande"),
                new DataColumn("Nombre de jour"),
                new DataColumn("Début"),
                new DataColumn("Fin"),
                new DataColumn("Motif de la demande"),
                new DataColumn("Status de la demande"),
                new DataColumn("Type de congé")
            });
            return dataTable;
        }

        private DataTable CreateDataTableForLeaveBalance()
        {
            DataTable dataTable = new DataTable("LeaveBalances");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nom & Prénoms"),
                new DataColumn("Jour par an"),
                new DataColumn("Jours Disponibles"),
                new DataColumn("Jours Utilisés"),
            });
            return dataTable;
        }


        private FileResult File(byte[] bytes, string contentType, string fileName)
        {
            // Implémentez la logique pour créer et retourner un FileResult avec les paramètres fournis
            return new FileContentResult(bytes, contentType)
            {
                FileDownloadName = fileName
            };
        }

    }
}
