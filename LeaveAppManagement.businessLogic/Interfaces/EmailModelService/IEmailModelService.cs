using LeaveAppManagement.dataAccess.Models;


namespace LeaveAppManagement.businessLogic.Interfaces.EmailModelService
{
    public interface IEmailModelService
    {
        void SendEmail(EmailModel emailModel);
        Task SendEmailToConfirm(EmailModel emailModel, int id, CancellationToken cancellationToken);
    }
}
