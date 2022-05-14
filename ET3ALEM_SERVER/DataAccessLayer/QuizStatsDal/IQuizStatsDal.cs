using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataAccessLayer.QuizStatsDAL
{
    public interface IQuizStatsDal
    {
        Task<Question> GetMostMissedQuestionInQuiz(Quiz q);
    }
}