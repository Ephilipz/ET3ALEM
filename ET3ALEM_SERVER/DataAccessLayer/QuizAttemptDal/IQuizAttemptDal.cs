using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IQuizAttemptDal
    {
        Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt);
    }
}
