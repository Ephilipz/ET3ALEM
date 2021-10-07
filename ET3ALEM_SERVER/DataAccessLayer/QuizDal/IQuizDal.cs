using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;

namespace DataAccessLayer
{
    public interface IQuizDal
    {
        Task<Quiz> GetQuiz(int quizId);
        Task<string> GetQuizTitleFromCode(string code);
        Task<Quiz> GetBasicQuizByCode(string code);
        Task<Quiz> GetFullQuizByCode(string code);
        Task<Quiz> GetSimpleQuiz(int quizId);
        Task<List<Quiz>> GetQuizzes(string userId);
        Task<Quiz> InsertQuiz(Quiz quiz);
        Task<Quiz> DeleteQuiz(int id);
        Task PutQuiz(Quiz quiz);
        Task<List<QuizAttempt>> GetUngradedQuizzesForUser(string userId);
    }
}