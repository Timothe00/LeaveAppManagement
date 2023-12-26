﻿using LeaveAppManagement.businessLogic.Interfaces;
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
                
                DateStart = x.DateStart,
                DateEnd = x.DateEnd,

                NumberOfDays = (x.DateEnd - x.DateStart).Days,

                Commentary = x.Commentary,
                RequestStatus = x.RequestStatus,
                LeaveTypeName = x.LeaveTypeName,
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,

            });
            return leaveRequestDtos;
        }

        public async Task<LeaveRequestDto> GetLeaveRequestByIdServicAsync(int Id, CancellationToken cancellationToken)
        {
            var leave = await _iLeaveRequestRepository.GetSingleLeaveRequestAsync(Id, cancellationToken);

            LeaveRequestDto LeaveRequest = new()
            {
                Id = leave.Id,
                DateRequest = leave.DateRequest.Date,
                
                DateStart = leave.DateStart.Date,
                DateEnd = leave.DateEnd.Date,

                NumberOfDays = (leave.DateEnd - leave.DateStart).Days,

                Commentary = leave.Commentary,
                RequestStatus = leave.RequestStatus,
                LeaveTypeName = leave.LeaveTypeName,

                EmployeeId = leave.EmployeeId,

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
                DateRequest = leaveRequest.DateRequest,
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
                DateStart = updateLeaveRequestDto.DateStart,
                DateEnd = updateLeaveRequestDto.DateEnd,
                Commentary = updateLeaveRequestDto.Commentary,
                LeaveTypeId = updateLeaveRequestDto.LeaveTypeId
            };
            await _iLeaveRequestRepository.UpdateLeaveRequestAsync(updateLeaveRequestDto, cancellationToken);
            return leaveRequest;
        }


        public async Task<LeaveRequest> UpdateLeaveRequestStatusServiceAsync(RequestStatusDto requestStatusDto, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = new LeaveRequest
            {
                Id = requestStatusDto.Id,
                RequestStatus = requestStatusDto.RequestStatus,
            };
            await _iLeaveRequestRepository.UpdateRequestStatus(requestStatusDto, cancellationToken);
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
