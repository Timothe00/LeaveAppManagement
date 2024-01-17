using LeaveAppManagement.dataAccess.Dto;

namespace LeaveAppManagement.dataAccess.Interfaces
{
    public interface IAllRequestsAcceptedRepository
    {
        Task<IEnumerable<AllRequestAcceptedDto>> GetAllRequestsAccepted(CancellationToken cancellationToken);
    }
}
