
using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Services
{
    public class LeaveBalanceService : ILeaveBalenceService
    {
        private readonly ILeaveBalanceRepository _ileaveBalanceRepository;

        public LeaveBalanceService(ILeaveBalanceRepository ileaveBalanceRepository)
        {
            _ileaveBalanceRepository = ileaveBalanceRepository;
        }


        public async Task<LeaveBalanceDto> GetAllLeaveBalanceService(int emplId, CancellationToken cancellationToken)
        {
            LeaveBalanceDto leaveBalance = await _ileaveBalanceRepository.GetLeaveBalanceAsync(emplId, cancellationToken);
            return leaveBalance;
        }
    }
}
