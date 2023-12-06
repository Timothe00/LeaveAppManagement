using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.businessLogic.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _iLeaveRequestRepository;

        public LeaveRequestService(ILeaveRequestRepository iLeaveRequestRepository)
        {
            _iLeaveRequestRepository = iLeaveRequestRepository;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestServiceAsync(CancellationToken cancellationToken)
        {
            IEnumerable<LeaveRequestDto> leaves = await _iLeaveRequestRepository.GetLeaveRequestAsync(cancellationToken);

            IEnumerable<LeaveRequestDto> leaveRequestDtos = leaves.Select(x => new LeaveRequestDto
            {
                Id = x.Id,
                DateRequest = x.DateRequest,
                NumberOfDays = x.NumberOfDays,
                DateStart = x.DateStart,
                DateEnd = x.DateEnd,
                Commentary = x.Commentary,
                RequestStatus = x.RequestStatus,
                LeaveTypeName = x.LeaveTypeName,
                FirstName = x.FirstName,
                LastName = x.LastName,

            });
            return leaveRequestDtos;
        }

        public async Task<LeaveRequestDto> GetLeaveRequestByIdServicAsync(int Id, int leaveTypeId, CancellationToken cancellationToken)
        {
            var leave = await _iLeaveRequestRepository.GetSingleLeaveRequestAsync(Id, leaveTypeId, cancellationToken);

            LeaveRequestDto LeaveRequest = new()
            {
                Id = leave.Id,
                DateRequest = leave.DateRequest,
                NumberOfDays = leave.NumberOfDays,
                DateStart = leave.DateStart,
                DateEnd = leave.DateEnd,
                Commentary = leave.Commentary,
                RequestStatus = leave.RequestStatus,
                LeaveTypeName = leave.LeaveTypeName,
                FirstName = leave.FirstName,
                LastName = leave.LastName,

            };
            return LeaveRequest;
        }

        public async Task<LeaveRequest> AddLeaveRequestServiceAsync(PosteLeaveRequestDto leaveRequest, CancellationToken cancellationToken)
        {
            // Vérifie que LeaveType et EmployeeId ne sont pas nuls
            if (leaveRequest.LeaveTypeId == 0 || leaveRequest.EmployeeId == 0)
            {
                throw new ArgumentException("LeaveTypeId and EmployeeId cannot be null.");

            }

            var newRequest = new LeaveRequest
            {
                Id = leaveRequest.Id,
                DateRequest = leaveRequest.DateRequest,
                NumberOfDays = leaveRequest.NumberOfDays,
                DateStart = leaveRequest.DateStart,
                DateEnd = leaveRequest.DateEnd,
                Commentary = leaveRequest.Commentary,
                RequestStatus = leaveRequest.RequestStatus,
                EmployeeId = leaveRequest.EmployeeId,
                LeaveTypeId = leaveRequest.LeaveTypeId,
            };


            await _iLeaveRequestRepository.AddLeaveRequestAsync(newRequest);
            return newRequest;
        }


        public async Task<LeaveRequest> UpdateLeaveRequestServiceAsync(UpdateLeaveRequestDto updateLeaveRequestDto, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = new LeaveRequest
            {
                Id = updateLeaveRequestDto.Id,
                DateRequest = updateLeaveRequestDto.DateRequest,
                NumberOfDays = updateLeaveRequestDto.NumberOfDays,
                DateStart = updateLeaveRequestDto.DateStart,
                DateEnd = updateLeaveRequestDto.DateEnd,
                Commentary = updateLeaveRequestDto.Commentary,
                RequestStatus = updateLeaveRequestDto.RequestStatus,
                LeaveTypeId = updateLeaveRequestDto.LeaveTypeId
            };
            await _iLeaveRequestRepository.UpdateLeaveRequestAsync(updateLeaveRequestDto, cancellationToken);
            return leaveRequest;
        }

        public async Task<bool> DeleteLeaveRequestAsyncServiceAsync(int reqId)
        {
            if (reqId < 0)
            {
                return false;
            }
            await _iLeaveRequestRepository.DeleteLeaveRequestAsync(reqId);
            return true;
        }


    }
}
