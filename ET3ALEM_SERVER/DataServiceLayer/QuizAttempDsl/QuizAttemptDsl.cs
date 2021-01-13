using BusinessEntities.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class QuizAttemptDsl : IQuizAttemptDsl
    {
        private IQuizAttemptDal _IQuizAttemptDal;
        public QuizAttemptDsl(IQuizAttemptDal IQuizAttemptDal)
        {
            _IQuizAttemptDal = IQuizAttemptDal;
        }
        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            return await _IQuizAttemptDal.InsertQuizAttempt(quizAttempt);
        }
    }
}
