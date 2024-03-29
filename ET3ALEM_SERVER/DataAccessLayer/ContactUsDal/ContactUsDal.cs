﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

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
            _context.ContactUsMessages.Add(contactUsMessage);
            await _context.SaveChangesAsync();
            return contactUsMessage;
        }

        public async Task<List<ContactUsMessage>> GetAllContactUsMessages()
        {
            return await _context.ContactUsMessages.ToListAsync();
        }
    }
}