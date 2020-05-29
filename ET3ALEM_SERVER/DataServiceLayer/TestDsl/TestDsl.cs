using DataAccessLayer.TestDataAccess;
using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.TestDataService
{
    public class TestDsl : ITestDsl
    {
        private ITestDal _TestDal;
        public TestDsl(ITestDal TestDal)
        {
            _TestDal = TestDal;
        }

        public Task<List<Test>> GetTests()
        {
            return _TestDal.GetTests();
        }
    }
}
