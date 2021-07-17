﻿using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ContactUsDal : IContactUsDal
    {
        private readonly ApplicationContext _context;
        public ContactUsDal(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<ContactUsMessage> InsertContactUsMessage(ContactUsMessage contactUsMessage)
        {
            await _context.ContactUsMessages.AddAsync(contactUsMessage);
            return contactUsMessage;
        }
        public async Task<List<ContactUsMessage>> GetAllContactUsMessages()
        {
            return await _context.ContactUsMessages.ToListAsync();
        }
    }
}
