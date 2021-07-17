using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IContactUsDal
    {
        Task<ContactUsMessage> InsertContactUsMessage(ContactUsMessage contactUs);
        Task<List<ContactUsMessage>> GetAllContactUsMessages();
    }
}
