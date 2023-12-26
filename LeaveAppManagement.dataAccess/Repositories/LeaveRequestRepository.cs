using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;

        public LeaveRequestRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestAsync(CancellationToken cancellationToken)
        {
            var leaveRequests = await _dbContext.LeaveRequests.ToListAsync(cancellationToken);
            var users = await _dbContext.Users.Where(user => user.RoleId == 3).ToListAsync(cancellationToken);
            var leaveTypes = await _dbContext.LeaveTypes.ToListAsync(cancellationToken);

            var EmployeeReq = from leave in leaveRequests
                              join user in users 
                              on leave.EmployeeId 
                              equals user.Id
                              join leaveType in leaveTypes on leave.LeaveTypeId equals leaveType.Id
                              select new LeaveRequestDto
                              {
                                  Id = leave.Id,
                                  DateRequest = leave.DateRequest,
                                  
                                  DateStart = leave.DateStart,
                                  DateEnd = leave.DateEnd,

                                  NumberOfDays = (leave.DateEnd - leave.DateStart).Days,

                                  Commentary = leave.Commentary,
                                  RequestStatus = leave.RequestStatus,
                                  EmployeeId = leave.EmployeeId,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  LeaveTypeName = leaveType.LeaveTypeName,
                              };
            return EmployeeReq;
            
        }




        public async Task<LeaveRequestDto> GetSingleLeaveRequestAsync(int Id, CancellationToken cancellationToken)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Where(lr => lr.Id == Id)
                .Join(_dbContext.Users,
                    leave => leave.EmployeeId,
                    user => user.Id,
                    (leave, user) => new { Leave = leave, User = user })
                .Join(_dbContext.LeaveTypes,
                    temp0 => temp0.Leave.LeaveTypeId,
                    leaveType => leaveType.Id,
                    (temp0, leaveType) => new LeaveRequestDto
                    {
                        Id = temp0.Leave.Id,
                        DateRequest = temp0.Leave.DateRequest.Date,
                        DateStart = temp0.Leave.DateStart.Date,
                        DateEnd = temp0.Leave.DateEnd.Date,
                        NumberOfDays = (temp0.Leave.DateEnd - temp0.Leave.DateStart).Days,
                        Commentary = temp0.Leave.Commentary,
                        RequestStatus = temp0.Leave.RequestStatus,
                        EmployeeId = temp0.Leave.EmployeeId,
                        FirstName = temp0.User.FirstName,
                        LastName = temp0.User.LastName,
                        LeaveTypeName = leaveType.LeaveTypeName
                    })
                .SingleOrDefaultAsync(cancellationToken);

            return leaveRequest;
        }


        public async Task<LeaveRequest> AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _ = _dbContext.LeaveRequests.Add(leaveRequest);
            await _dbContext.SaveChangesAsync();
            return leaveRequest;
        }


        public async Task<LeaveRequest> UpdateLeaveRequestAsync(UpdateLeaveRequestDto updatReq, CancellationToken cancellation)
        {
            var leaveRequest = await _dbContext.LeaveRequests.Where(lr => lr.Id == updatReq.Id).FirstOrDefaultAsync(cancellation);
            
            if (leaveRequest!=null)
            {
                leaveRequest.Id = updatReq.Id;
                leaveRequest.DateRequest = updatReq.DateRequest.Date;
                leaveRequest.DateStart = updatReq.DateStart.Date;
                leaveRequest.DateEnd = updatReq.DateEnd.Date;
                leaveRequest.Commentary = updatReq.Commentary;
                leaveRequest.LeaveTypeId = updatReq.LeaveTypeId;

                _dbContext.LeaveRequests.Update(leaveRequest);
                await _dbContext.SaveChangesAsync();
            }

            return null;
        }


        public async Task<LeaveRequest> UpdateRequestStatus(RequestStatusDto requestStatusDto, CancellationToken cancellationToken)
        {
            var leaveRequest = await _dbContext.LeaveRequests.Where(lr => lr.Id == requestStatusDto.Id).FirstOrDefaultAsync(cancellationToken);

            if (leaveRequest != null)
            {
                leaveRequest.Id = requestStatusDto.Id;
                leaveRequest.RequestStatus = requestStatusDto.RequestStatus;
                _dbContext.LeaveRequests.Update(leaveRequest);
                await _dbContext.SaveChangesAsync();
            }

            return null;
        }

        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests.FindAsync(id);

            if (leaveRequest != null)
            {
                _dbContext.LeaveRequests.Remove(leaveRequest);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
