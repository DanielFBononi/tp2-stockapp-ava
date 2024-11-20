using Microsoft.Extensions.Options;
using StockApp.Domain.Interfaces;
using System.Net.Mail;

namespace StockApp.Infra.Data.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            var emailClient = new SmtpClient("localhost", 25);

            var message = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body
            };
            message.To.Add(new MailAddress(to));
            await emailClient.SendMailAsync(message);
        }
    }
}
    
