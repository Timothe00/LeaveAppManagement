using LeaveAppManagement.dataAccess.Data;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace LeaveAppManagement.dataAccess.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly LeaveAppManagementDbContext _dbContext;
        public LeaveTypeRepository(LeaveAppManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<LeaveType>> GetLeaveTypeAsync(CancellationToken cancellationToken)
        {
            IEnumerable<LeaveType> leaveType = await _dbContext.LeaveTypes.ToListAsync(cancellationToken);
            return leaveType;
        }

        public async Task<LeaveType?> GetSingleLeaveTypeAsync(int id, CancellationToken cancellationToken)
        {
            var leaveType = await _dbContext.LeaveTypes.FindAsync(id, cancellationToken);
            return leaveType;
        }


        public async Task<LeaveType> AddLeaveTypeAsync(LeaveType NewleaveType, CancellationToken cancellationToken)
        {
            _dbContext.LeaveTypes.Add(NewleaveType);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return NewleaveType;
        }


        public async Task<LeaveType> UpdateLeaveTypeAsync(LeaveType UpdateleaveType, CancellationToken cancellationToken)
        {
            var NewType = await _dbContext.LeaveTypes.Where(lt => lt.Id == UpdateleaveType.Id).FirstOrDefaultAsync(cancellationToken);
            if (NewType != null)
            {
                NewType.Id = UpdateleaveType.Id;
                NewType.LeaveTypeName = UpdateleaveType.LeaveTypeName;

                _dbContext.Update(NewType);
                await _dbContext.SaveChangesAsync();
            }

            return null;
        }

        public async Task<bool> DeleteLeaveTypeAsync(int id, CancellationToken cancellationToken)
        {
            var leaveType = await _dbContext.LeaveTypes.FindAsync(id);

            if (leaveType == null)
            {
                // Si l'objet n'est pas trouvé, renvoyer false
                return false;
            }

            _dbContext.LeaveTypes.Remove(leaveType);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
