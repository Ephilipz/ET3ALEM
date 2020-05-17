using Server_Application.BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.TestDataService
{
    public interface ITestDsl
    {
        Task<List<Test>> GetTests();
    }
}
