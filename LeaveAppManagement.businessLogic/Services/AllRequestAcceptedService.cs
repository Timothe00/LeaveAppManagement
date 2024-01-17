using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Dto;
using LeaveAppManagement.dataAccess.Interfaces;



namespace LeaveAppManagement.businessLogic.Services
{
    public class AllRequestAcceptedService : IAllRequestAcceptedService
    {
        private readonly IAllRequestsAcceptedRepository _iAllRequestsAcceptedRepository;

        public AllRequestAcceptedService(IAllRequestsAcceptedRepository iAllRequestsAcceptedRepository)
        {
            _iAllRequestsAcceptedRepository = iAllRequestsAcceptedRepository;
        }

        public async Task<IEnumerable<AllRequestAcceptedDto>> GetAllAcceptedReqAsync(CancellationToken cancellationToken)
        {
            IEnumerable<AllRequestAcceptedDto> leavesaccept = await _iAllRequestsAcceptedRepository.GetAllRequestsAccepted(cancellationToken);
            return leavesaccept;
        }

    }
}
