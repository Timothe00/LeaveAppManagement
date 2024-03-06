using LeaveAppManagement.businessLogic.Interfaces;
using LeaveAppManagement.businessLogic.Interfaces.EmailModelService;
using LeaveAppManagement.dataAccess.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace LeaveAppManagement.businessLogic.Services
{
    public class EmailModelService : IEmailModelService
    {
        private readonly IConfiguration _config;
        private readonly IUsersService _usersService;
        public EmailModelService(IConfiguration config, IUsersService usersService)
        {
            _config = config;
            _usersService = usersService;
        }

        public void SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _config["EmailSettings:From"];
            emailMessage.From.Add(new MailboxAddress("InFi SoFtware", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
            emailMessage.Subject = emailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content)
            };

            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config["EmailSettings:SmtpServer"], 465, true);
                    client.Authenticate(_config["EmailSettings:From"], _config["EmailSettings:Password"]);
                    client.Send(emailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                finally 
                {
                    client.Disconnect(true);
                    client.Dispose(); 
                }
            }
        }


        public async Task SendEmailToConfirm(EmailModel emailModel, int id, CancellationToken cancellationToken)
        {
            var emailMessage = new MimeMessage();
            var user = await _usersService.GetUserServiceByIdAsync(id, cancellationToken);
            var managers = await _usersService.GetManagersServiceAsync(cancellationToken);

            var from = new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email);

            emailMessage.From.Add(from);

            foreach (var manager in managers)
            {
                var to = new MailboxAddress($"{manager.FirstName} {manager.LastName}", manager.Email);
                emailMessage.To.Add(to);
            }

            emailMessage.Subject = emailModel.Subject;

            // Ajoutez l'adresse e-mail de l'expéditeur dans le champ "Reply-To"
            emailMessage.ReplyTo.Add(from);

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content)
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_config["EmailSettings:SmtpServer"], 465, true);
                    await client.AuthenticateAsync(_config["EmailSettings:From"], _config["EmailSettings:Password"]);
                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    // Gérez les exceptions ici
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }


    }
}
