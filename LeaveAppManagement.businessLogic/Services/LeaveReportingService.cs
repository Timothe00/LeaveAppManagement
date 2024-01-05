

using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Interfaces;

namespace LeaveAppManagement.businessLogic.Services
{
    public class LeaveReportingService : ILeaveReportingService
    {
        private readonly ILeaveReportingRepository _ileaveReportingRepository;

        public LeaveReportingService(ILeaveReportingRepository ileaveReportingRepository)
        {
            _ileaveReportingRepository = ileaveReportingRepository;
        }


    }
}
