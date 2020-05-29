using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QuestionDataAccess
{
    public interface IQuestionDal
    {
        Task<List<Question>> GetQuestions();
    }
}
