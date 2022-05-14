using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

namespace DataAccessLayer.QuizStatsDAL
{
    public class QuizStatsDal : IQuizStatsDal
    {
        private readonly ApplicationContext _context;

        public QuizStatsDal(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<Question> GetMostMissedQuestionInQuiz(Quiz q)
        {
            var question = await _context.Quizzes
                .Where(quiz => quiz == q)
                .SelectMany(quiz => quiz.QuizAttempts)
                .SelectMany(quizAttempt => quizAttempt.QuestionsAttempts)
                .Where(questionAttempt => questionAttempt.IsGraded &&
                                          questionAttempt.Grade <
                                          questionAttempt.QuizQuestion.Grade)
                .GroupBy(questionAttempt =>
                    questionAttempt.QuizQuestion.Question)
                .OrderByDescending(grouping => grouping.Count())
                .Select(grouping => grouping.Key)
                .FirstOrDefaultAsync();
            return question;
        }
    }
}