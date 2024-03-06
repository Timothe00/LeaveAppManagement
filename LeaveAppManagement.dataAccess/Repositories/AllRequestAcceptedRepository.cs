using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaveAppManagement.dataAccess.Repositories
{
    public class AllRequestAcceptedRepository : IAllRequestsAcceptedRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;
        private enum UserRoles { Admin = 1, Manager, Employee }
        private readonly string REQUEST_STATUS = "Acceptée";

        public AllRequestAcceptedRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<AllRequestAcceptedDto>> GetAllRequestsAccepted(CancellationToken cancellationToken)
        {
            // Récupération des données de la base de données de manière asynchrone
            var leaveRequests = await _dbContext.LeaveRequests.Where(lr => lr.RequestStatus == REQUEST_STATUS).ToListAsync(cancellationToken);
            var users = await _dbContext.Users.Where(user => user.RoleId == (int)UserRoles.Manager ||user.RoleId == (int)UserRoles.Employee).ToListAsync(cancellationToken);
            var leaveTypes = await _dbContext.LeaveTypes.ToListAsync(cancellationToken);

            var allReq = from leave in leaveRequests
                         join user in users
                         on leave.EmployeeId
                         equals user.Id
                         join leaveType in leaveTypes on leave.LeaveTypeId equals leaveType.Id

                         select new AllRequestAcceptedDto
                         {
                             Start = leave.DateStart,
                             End = leave.DateEnd,
                             Title = string.Concat(user.FirstName, ' ', user.LastName)
                         };

            return allReq;
        }
    }
}
