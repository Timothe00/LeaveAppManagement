using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.EntityFrameworkCore;

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
                                  NumberOfDays = leave.NumberOfDays,
                                  DateStart = leave.DateStart,
                                  DateEnd = leave.DateEnd,
                                  Commentary = leave.Commentary,
                                  RequestStatus = leave.RequestStatus,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  LeaveTypeName = leaveType.LeaveTypeName,
                              };
            return EmployeeReq;
            
        }




        public async Task<LeaveRequestDto> GetSingleLeaveRequestAsync(int userId, int leaveTypeId, CancellationToken cancellationToken)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Where(request => request.EmployeeId == userId && request.LeaveTypeId == leaveTypeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (leaveRequest == null)
            {
                // La demande de congé n'a pas été trouvée.
                return null;
            }

            var user = await _dbContext.Users
                .Where(user => user.Id == userId && user.RoleId == 3)
                .FirstOrDefaultAsync(cancellationToken);

            var leaveType = await _dbContext.LeaveTypes
                .Where(type => type.Id == leaveTypeId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null || leaveType == null)
            {
                // L'utilisateur ou le type de congé n'a pas été trouvé.
                return null;
            }

            var leaveRequestDto = new LeaveRequestDto
            {
                Id = leaveRequest.Id,
                DateRequest = leaveRequest.DateRequest,
                NumberOfDays = leaveRequest.NumberOfDays,
                DateStart = leaveRequest.DateStart,
                DateEnd = leaveRequest.DateEnd,
                Commentary = leaveRequest.Commentary,
                RequestStatus = leaveRequest.RequestStatus,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LeaveTypeName = leaveType.LeaveTypeName,
            };

            return leaveRequestDto;
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
                leaveRequest.DateRequest = updatReq.DateRequest;
                leaveRequest.NumberOfDays = updatReq.NumberOfDays;
                leaveRequest.DateStart = updatReq.DateStart;
                leaveRequest.DateEnd = updatReq.DateEnd;
                leaveRequest.Commentary = updatReq.Commentary;
                leaveRequest.LeaveTypeId = updatReq.LeaveTypeId;

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
