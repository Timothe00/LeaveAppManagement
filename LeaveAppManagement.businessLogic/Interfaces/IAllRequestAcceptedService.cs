using LeaveAppManagement.dataAccess.Dto;

namespace LeaveAppManagement.businessLogic.Interfaces
{
    public interface IAllRequestAcceptedService
    {
        Task<IEnumerable<AllRequestAcceptedDto>> GetAllAcceptedReqAsync(CancellationToken cancellationToken);
    }
}
