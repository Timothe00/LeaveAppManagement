

using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.dataAccess.Interfaces;
using LeaveAppManagement.dataAccess.Models;

namespace LeaveAppManagement.businessLogic.Services
{
    public class LeaveReportingService : ILeaveReportingService
    {
        private readonly ILeaveReportingRepository _ileaveReportingRepository;

        public LeaveReportingService(ILeaveReportingRepository ileaveReportingRepository)
        {
            _ileaveReportingRepository = ileaveReportingRepository;
        }


        public async Task<LeaveReporting> GetAllLeaveStatisticsAsync(string role, CancellationToken cancellationToken)
        {
            var statistics = new LeaveReporting();
            int year = DateTime.Now.Year;

            statistics.CurrentYear = year;
            // Récupérer le nombre total de congés
            statistics.TotalRequest = await _ileaveReportingRepository.GetTotalTotalLeaveRequestAsync(role, cancellationToken);

            // Récupérer le nombre total de congés en attente
            statistics.TotalPending = await _ileaveReportingRepository.GetTotalPendingLeaveAsync(cancellationToken);

            // Récupérer le nombre total de congés approuvés
            statistics.TotalApproved = await _ileaveReportingRepository.GetTotalAcceptedLeaveAsync(cancellationToken);

            // Récupérer le nombre total de congés refusés
            statistics.TotalRejected = await _ileaveReportingRepository.GetTotalRejectedLeaveAsync(cancellationToken);

            return statistics;
        }


        public async Task<LeaveReporting> GetUserLeaveStatisticsAsync(int userId, CancellationToken cancellationToken)
        {
            var statistics = new LeaveReporting();
            int year = DateTime.Now.Year;

            statistics.CurrentYear = year;
            // Récupérer le nombre total de congés pour l'utilisateur spécifié
            statistics.TotalRequest = await _ileaveReportingRepository.GetTotalTotalLeaveRequestForSingleUserAsync(userId, cancellationToken);

            // Récupérer le nombre total de congés en attente pour l'utilisateur spécifié
            statistics.TotalPending = await _ileaveReportingRepository.GetTotalPendingLeaveForSingleUserAsync(userId, cancellationToken);

            // Récupérer le nombre total de congés approuvés pour l'utilisateur spécifié
            statistics.TotalApproved = await _ileaveReportingRepository.GetTotalAcceptedLeaveForSingleUserAsync(userId, cancellationToken);

            // Récupérer le nombre total de congés refusés pour l'utilisateur spécifié
            statistics.TotalRejected = await _ileaveReportingRepository.GetTotalRejectedLeaveForSingleUserAsync(userId, cancellationToken);

            return statistics;
        }

    }
}
