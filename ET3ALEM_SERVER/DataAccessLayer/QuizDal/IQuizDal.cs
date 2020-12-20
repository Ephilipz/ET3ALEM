using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IQuizDal
    {
        Task<Quiz> GetQuiz(int quizId);
        Task<string> GetQuizTitleFromCode(string code);
        Task<Quiz> GetSimpleQuiz(int quizId);
        Task<IEnumerable<Quiz>> GetQuizzes(string userId);
        Task<Quiz> InsertQuiz(Quiz quiz);
        Task<Quiz> DeleteQuiz(int id);
        Task PutQuiz(int id, Quiz quiz);
    }
}
