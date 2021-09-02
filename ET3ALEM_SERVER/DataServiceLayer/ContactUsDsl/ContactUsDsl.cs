using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataAccessLayer;

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