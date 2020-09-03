using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IQuizDsl
    {
        Task<Quiz> GetQuiz(int quizId);
        Task<IEnumerable<Quiz>> GetQuizzes();
        Task<Quiz> InsertQuiz(Quiz quiz);
        Task<Quiz> DeleteQuiz(int id);
        Task PutQuiz(int id, Quiz quiz);
    }
}
