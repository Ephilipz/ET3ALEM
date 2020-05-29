using DataAccessLayer.QuestionDataAccess;
using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.QuestionDataService
{
    public class QuestionDsl : IQuestionDsl
    {
        private IQuestionDal _QuestionDal;
        public QuestionDsl(IQuestionDal QuestionDal)
        {
            _QuestionDal = QuestionDal;
        }
        public Task<List<Question>> GetQuestions()
        {
            return _QuestionDal.GetQuestions();
        }
    }
}
