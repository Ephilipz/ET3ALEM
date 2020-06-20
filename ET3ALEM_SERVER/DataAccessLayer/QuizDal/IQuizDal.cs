using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QuizDal
{
    public interface IQuizDal
    {
        Task<Quiz> GetQuiz(int quizId);
        Task<Quiz> InsertQuiz(Quiz quiz);
    }
}
