using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Role_Identity.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Role_Identity.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly MailSettings _mailSettings;

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string htmlMessage)
        {
            string mail = _mailSettings.Mail;
            string password = _mailSettings.Password;
            string host = _mailSettings.Host;
            int port = _mailSettings.Port;


            var Message = new MailMessage();
            Message.From = new MailAddress(mail);
            Message.Subject = subject;
            Message.To.Add(mailTo);
            Message.Body = $"<html><body>{htmlMessage}</body></html>";
            Message.IsBodyHtml = true;

            var smtpClient = new SmtpClient()
            {
                Host = host,
                Port = port,
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true
            };
            smtpClient.Send(Message);
        }
    }
}
