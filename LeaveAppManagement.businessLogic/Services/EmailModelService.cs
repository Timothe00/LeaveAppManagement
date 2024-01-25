﻿using LeaveAppManagement.businessLogic.Interfaces.EmailModelService;
using LeaveAppManagement.dataAccess.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace LeaveAppManagement.businessLogic.Services
{
    public class EmailModelService : IEmailModelService
    {
        private readonly IConfiguration _config;
        public EmailModelService(IConfiguration config)
        {
            _config = config;
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
    }
}
