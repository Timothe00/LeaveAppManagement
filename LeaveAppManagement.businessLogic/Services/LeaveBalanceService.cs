
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


        public async Task<IEnumerable<LeaveBalanceDto>> GetLeaveBalanceForEmployeesServiceAsync(CancellationToken cancellationToken)
        {
            IEnumerable<LeaveBalanceDto> leaveBalanceDtos = await _ileaveBalanceRepository.GetAllLeaveBalanceForAllEmployeeAsync(cancellationToken);
            return leaveBalanceDtos;
        }

        public async Task<LeaveBalanceDto> GetAllLeaveBalanceService(int emplId, CancellationToken cancellationToken)
        {
            LeaveBalanceDto leaveBalance = await _ileaveBalanceRepository.GetLeaveBalanceAsync(emplId, cancellationToken);
            return leaveBalance;
        }

    }
}
