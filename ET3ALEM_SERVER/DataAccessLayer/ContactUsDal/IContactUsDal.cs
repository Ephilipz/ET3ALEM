using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataAccessLayer
{
    public interface IContactUsDal
    {
        Task<ContactUsMessage> InsertContactUsMessage(ContactUsMessage contactUs);
        Task<List<ContactUsMessage>> GetAllContactUsMessages();
    }
}