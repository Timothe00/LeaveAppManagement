﻿using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface ILeaveTypeRepository
    {
        Task<IEnumerable<LeaveType>> GetLeaveTypeAsync(CancellationToken cancellationToken);
        Task<LeaveType?> GetSingleLeaveTypeAsync(int id, CancellationToken cancellationToken);
        Task<LeaveType> AddLeaveTypeAsync(LeaveType NewleaveType, CancellationToken cancellationToken);
        Task<LeaveType> UpdateLeaveTypeAsync(LeaveType UpdateleaveType, CancellationToken cancellationToken);
        Task<bool> DeleteLeaveTypeAsync(int id, CancellationToken cancellationToken);
    }
}
