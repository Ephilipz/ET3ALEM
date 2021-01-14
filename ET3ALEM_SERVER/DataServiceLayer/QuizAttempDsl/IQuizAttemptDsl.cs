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
    }
}
