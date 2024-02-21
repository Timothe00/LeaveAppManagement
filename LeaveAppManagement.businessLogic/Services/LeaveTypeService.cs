using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.businessLogic.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        public readonly ILeaveTypeRepository _ileaveTypeRepository;
        public LeaveTypeService(ILeaveTypeRepository ileaveTypeRepository) 
        {
            _ileaveTypeRepository = ileaveTypeRepository;
        }

        public async Task<IEnumerable<LeaveTypeDto>> GetLeaveTypeServiceAsync(CancellationToken cancellationToken)
        {
            IEnumerable<LeaveType> leaveType = await _ileaveTypeRepository.GetLeaveTypeAsync(cancellationToken);

            IEnumerable<LeaveTypeDto> leaveTypedto = leaveType.Select(lt => new LeaveTypeDto
            {
                Id = lt.Id,
                LeaveTypeName = lt.LeaveTypeName,
            });
            return leaveTypedto;
        }


        public async Task<LeaveTypeDto> GetLeaveTypeByIdServiceAsync(int id, CancellationToken cancellationToken)
        {
            var leaveType = await _ileaveTypeRepository.GetSingleLeaveTypeAsync(id, cancellationToken);

            var singleleaveType = new LeaveTypeDto
            {
                Id = leaveType.Id,
                LeaveTypeName = leaveType.LeaveTypeName,
            };

            return singleleaveType;
        }


        public async Task<LeaveType> AddLeaveTypeServiceAsync(LeaveTypeDto leaveTypedto, CancellationToken cancellationToken)
        {
            LeaveType leaveType = new();
            leaveType.Id = leaveTypedto.Id;
            leaveType.LeaveTypeName = leaveTypedto.LeaveTypeName;

            return await _ileaveTypeRepository.AddLeaveTypeAsync(leaveType, cancellationToken);
        }


        public async Task<LeaveType> UpdateLeaveTypeServiceAsync(LeaveTypeDto leaveTypedto, CancellationToken cancellationToken)
        {
            LeaveType leaveType = new();

            leaveType.Id = leaveTypedto.Id;
            leaveType.LeaveTypeName = leaveTypedto.LeaveTypeName;
            
            await _ileaveTypeRepository.UpdateLeaveTypeAsync(leaveType, cancellationToken);
            return leaveType;
        }


        public async Task<bool> DeleteLeaveTypeAsyncServiceAsync(int leaveTypeId, CancellationToken cancellationToken)
        {
            if (leaveTypeId <= 0)
            {
                return false;
            }
            return await _ileaveTypeRepository.DeleteLeaveTypeAsync(leaveTypeId, cancellationToken);
        }


    }
}
