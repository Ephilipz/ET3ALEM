using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IContactUsDsl
    {
        Task<ContactUsMessage> InsertContactUsMessage(ContactUsMessage contactUs);
        Task<List<ContactUsMessage>> GetAllContactUsMessages();
    }
}
