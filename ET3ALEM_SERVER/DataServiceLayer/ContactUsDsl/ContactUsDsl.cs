using BusinessEntities.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class ContactUsDsl : IContactUsDsl
    {
        private readonly IContactUsDal _IContactUsDal;
        public ContactUsDsl(IContactUsDal IContactUsDal)
        {
            _IContactUsDal = IContactUsDal;
        }

        public async Task<List<ContactUsMessage>> GetAllContactUsMessages()
        {
            return await _IContactUsDal.GetAllContactUsMessages();
        }

        public async Task<ContactUsMessage> InsertContactUsMessage(ContactUsMessage contactUs)
        {
            return await _IContactUsDal.InsertContactUsMessage(contactUs);
        }
    }
}
