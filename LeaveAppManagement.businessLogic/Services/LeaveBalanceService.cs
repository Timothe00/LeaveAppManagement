
using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;


namespace LeaveAppManagement.businessLogic.Services
{
    public class LeaveBalanceService : ILeaveBalenceService
    {
        private readonly ILeaveBalanceRepository _ileaveBalanceRepository;

        public LeaveBalanceService(ILeaveBalanceRepository ileaveBalanceRepository)
        {
            _ileaveBalanceRepository = ileaveBalanceRepository;
        }


        public async Task<IEnumerable<LeaveBalanceDto>> GetLeaveBalanceServiceForEmployeeAsync(CancellationToken cancellationToken)
        {
            IEnumerable<LeaveBalanceDto> leaveBalanceDtos = await _ileaveBalanceRepository.GetAllEmployeeLeaveBalanceAsync(cancellationToken);
            return leaveBalanceDtos;
        }

        public async Task<LeaveBalanceDto> GetAllLeaveBalanceService(int emplId, CancellationToken cancellationToken)
        {
            LeaveBalanceDto leaveBalance = await _ileaveBalanceRepository.GetLeaveBalanceAsync(emplId, cancellationToken);
            return leaveBalance;
        }

    }
}
