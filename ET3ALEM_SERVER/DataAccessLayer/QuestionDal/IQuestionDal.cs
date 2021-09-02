using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

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