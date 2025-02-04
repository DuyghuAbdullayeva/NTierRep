using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.Helper.Implementations
{
    internal class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailConfig = _configuration.GetSection("Email");

            string host = emailConfig["SmtpServer"];
            int port = int.Parse(emailConfig["Port"]);
            bool enableSsl = bool.Parse(emailConfig["EnableSsl"]);
            string username = emailConfig["Username"];
            string password = emailConfig["Password"];
            string from = emailConfig["From"];

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host), "SMTP host cannot be null!");
            }

            using SmtpClient smtp = new SmtpClient(host)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl
            };

            using MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(_configuration["Email:Username"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
