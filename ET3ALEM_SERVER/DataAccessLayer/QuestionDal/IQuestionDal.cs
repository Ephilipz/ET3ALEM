using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IQuestionDal
    {
        Task<List<Question>> GetQuestions();
        Task<Question> InsertQuestion(Question question);
        Task<Question> DeleteQuestion(int questionId);
        Task PutQuestion(Question question);

    }
}
