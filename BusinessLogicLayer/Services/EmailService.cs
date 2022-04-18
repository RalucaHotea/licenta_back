using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Helpers;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration emailConfiguration;

        public EmailService(
           EmailConfiguration _emailConfiguration)
        {
            this.emailConfiguration = _emailConfiguration;
        }

        public async Task<bool> SendEmailAsync(EmailDetailsDto email)
        {
            var isEmailSend = false;
            MailMessage message = new MailMessage();
            try
            {
                message.To.Add(new MailAddress("fixed-term.Georgiana.Hotea@ro.bosch.com"));
                message.From = new MailAddress(emailConfiguration.From);
                message.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(email.MessageTemplate, new System.Net.Mime.ContentType("text/html"));
                message.AlternateViews.Add(htmlView);

                using (var smtp = new SmtpClient())
                {
                    smtp.Credentials = new NetworkCredential
                    {
                        UserName = emailConfiguration.From,
                    };
                    smtp.Host = emailConfiguration.Host;
                    smtp.Port = Convert.ToInt32(emailConfiguration.Port);
                    smtp.EnableSsl = true;


                    await smtp.SendMailAsync(message);
                    isEmailSend = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return isEmailSend;
        }
    }
}
