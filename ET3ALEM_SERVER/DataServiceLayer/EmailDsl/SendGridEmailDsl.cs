using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DataServiceLayer
{
    public class SendGridEmailDsl : IEmailDsl
    {
        private readonly IConfiguration _iConfiguration;
        private readonly string _apiKey;
        private readonly string _emailFrom;
        private readonly string _nameFrom;

        public SendGridEmailDsl(IConfiguration configuration)
        {
            _iConfiguration = configuration;
            _apiKey = _iConfiguration.GetSection("EmailConfiguration").GetValue<string>("ApiKey");
            _emailFrom = _iConfiguration.GetSection("EmailConfiguration").GetValue<string>("Sender");
            _nameFrom = _iConfiguration.GetSection("EmailConfiguration").GetValue<string>("User");
        }

        public Task SendEmail(string subject, string plainTextContent, string htmlContent, string emailTo,
            string nameTo = null, string emailFrom = null, string nameFrom = null)
        {
            EmailAddress from = new EmailAddress(emailFrom, nameFrom);
            EmailAddress to = new EmailAddress(emailTo, nameTo);
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            SendGridClient client = new SendGridClient(_apiKey);
            return client.SendEmailAsync(msg);
        }

        public Task SendEmail(string subject, string plainTextContent, string htmlContent, string emailTo,
            string nameTo = null)
        {
            return SendEmail(subject, plainTextContent, htmlContent, emailTo, nameTo, _emailFrom, _nameFrom);
        }
    }
}