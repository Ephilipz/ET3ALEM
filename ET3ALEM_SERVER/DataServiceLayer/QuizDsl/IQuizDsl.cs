using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.QuizDsl
{
    public interface IQuizDsl
    {
        Task<Quiz> GetQuiz(int quizId);
        Task<Quiz> InsertQuiz(Quiz quiz);
    }
}
