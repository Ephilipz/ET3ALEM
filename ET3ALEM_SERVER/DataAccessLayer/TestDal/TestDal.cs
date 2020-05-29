using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.TestDataAccess
{
    public class TestDal : ITestDal
    {
        private ApplicationContext _context;
        public TestDal(ApplicationContext context)
        {
            _context = context;
        }
        public Task<List<Test>> GetTests()
        {
            return _context.Tests.ToListAsync();
        }
    }
}
