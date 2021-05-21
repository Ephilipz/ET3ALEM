using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class SendGridEmailDsl : IEmailDsl
    {
        private readonly IConfiguration _IConfiguration;
        private readonly string ApiKey;
        private readonly string EmailFrom;
        private readonly string NameFrom;
        public SendGridEmailDsl(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
            ApiKey = _IConfiguration.GetSection("EmailConfiguration").GetValue<string>("ApiKey");
            EmailFrom = _IConfiguration.GetSection("EmailConfiguration").GetValue<string>("Sender");
            NameFrom = _IConfiguration.GetSection("EmailConfiguration").GetValue<string>("User");
        }

        public Task SendEmail(string subject, string plainTextContent, string htmlContent, string emailTo, string nameTo = null, string emailFrom = null, string nameFrom = null)
        {
            var Client = new SendGridClient(ApiKey);
            var from = new EmailAddress(emailFrom, nameFrom);
            var to = new EmailAddress(emailTo, nameTo);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return Client.SendEmailAsync(msg);
        }

        public Task SendEmail(string subject, string plainTextContent, string htmlContent, string emailTo, string nameTo = null)
        {
            return SendEmail(subject, plainTextContent, htmlContent, emailTo, nameTo, EmailFrom, NameFrom);
        }
    }
}
