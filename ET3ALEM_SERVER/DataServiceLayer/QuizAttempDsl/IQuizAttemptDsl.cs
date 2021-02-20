using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IQuizAttemptDsl
    {
        Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt);
        Task<QuizAttempt> GetQuizAttempt(int id);
        Task<QuizAttempt> PutQuizAttempt(int id, QuizAttempt quizAttempt);
        Task<List<QuizAttempt>> GetQuizAttemptsForQuiz(int quizId, string userId);
    }
}
