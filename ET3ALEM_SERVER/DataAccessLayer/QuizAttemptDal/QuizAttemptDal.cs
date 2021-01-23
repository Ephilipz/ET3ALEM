using BusinessEntities.Models;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class QuizAttemptDal : IQuizAttemptDal
    {
        private readonly ApplicationContext _context;
        public QuizAttemptDal(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            await _context.QuizAttempts.AddAsync(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }
    }
}
