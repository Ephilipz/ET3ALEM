using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataServiceLayer
{
    public interface IContactUsDsl
    {
        Task<ContactUsMessage> InsertContactUsMessage(ContactUsMessage contactUs);
        Task<List<ContactUsMessage>> GetAllContactUsMessages();
    }
}