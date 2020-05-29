using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.TestDataAccess
{
    public interface ITestDal
    {
        Task<List<Test>> GetTests();
    }
}
