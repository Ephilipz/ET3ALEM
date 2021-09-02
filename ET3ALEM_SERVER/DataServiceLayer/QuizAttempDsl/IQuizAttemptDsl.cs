using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataServiceLayer
{
    public interface IQuizAttemptDsl
    {
        Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt);
        Task<QuizAttempt> GetQuizAttempt(int id);
        Task<QuizAttempt> PostQuizAttempt(string userId, QuizAttempt quizAttempt);
        Task<QuizAttempt> UpdateQuizAttemptGrade(QuizAttempt quizAttempt);
        Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(int quizId, string userId);
        Task<List<QuizAttempt>> GetAllQuizAttemptsForQuiz(int quizId);
        Task<QuizAttempt> GetQuizAttemptWithQuiz(int id);
        Task<List<QuizAttempt>> GetQuizAttempts(string userId);
    }
}