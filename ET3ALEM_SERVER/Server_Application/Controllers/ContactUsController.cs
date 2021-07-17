using BusinessEntities.Models;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsDsl _IContactUsDsl;
        public ContactUsController(IContactUsDsl IContactUsDsl)
        {
            _IContactUsDsl = IContactUsDsl;
        }
        // GET: api/<ContactUsController>
        [HttpGet("GetAll")]
        public async Task<List<ContactUsMessage>> GetAll()
        {
            return await _IContactUsDsl.GetAllContactUsMessages();
        }

        //// GET api/<ContactUsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ContactUsController>
        [HttpPost]
        public async Task<ActionResult<ContactUsMessage>> Post(ContactUsMessage contactUs)
        {
            return await _IContactUsDsl.InsertContactUsMessage(contactUs);
        }

        //// PUT api/<ContactUsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ContactUsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
