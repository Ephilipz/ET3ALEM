using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IEmailDsl
    {
        Task SendEmail(string subject, string plainTextContent, string htmlContent, string emailTo,
            string nameTo = null, string emailFrom = null, string nameFrom = null);

        Task SendEmail(string subject, string plainTextContent, string htmlContent, string emailTo,
            string nameTo = null);
    }
}