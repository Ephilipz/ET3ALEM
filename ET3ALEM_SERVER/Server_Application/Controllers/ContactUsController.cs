using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server_Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsDsl _iContactUsDsl;

        public ContactUsController(IContactUsDsl contactUsDsl)
        {
            _iContactUsDsl = contactUsDsl;
        }

        [HttpGet("GetAll")]
        public async Task<List<ContactUsMessage>> GetAll()
        {
            return await _iContactUsDsl.GetAllContactUsMessages();
        }

        [HttpPost]
        public async Task<ActionResult<ContactUsMessage>> Post(ContactUsMessage contactUs)
        {
            return await _iContactUsDsl.InsertContactUsMessage(contactUs);
        }
    }
}