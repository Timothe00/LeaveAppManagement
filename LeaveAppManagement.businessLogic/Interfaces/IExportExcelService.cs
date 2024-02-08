using LeaveAppManagement.dataAccess.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IExportExcelService
    {
        FileResult GenerateExcel(string fileName, IEnumerable<LeaveRequestDto> leaveRequestDtos, IEnumerable<LeaveBalanceDto> LeaveBalanceDto);
    }
}
