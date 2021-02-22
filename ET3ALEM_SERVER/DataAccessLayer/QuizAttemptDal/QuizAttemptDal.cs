using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<QuizAttempt> GetQuizAttempt(int id)
        {
            return _context.QuizAttempts.Where(qA => qA.Id == id).Include(qA => qA.QuestionsAttempts).FirstAsync();
        }

        public async Task<QuizAttempt> PutQuizAttempt(int id,QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            foreach (var item in quizAttempt.QuestionsAttempts.OfType<MCQAttmept>().SelectMany(mcqA=>mcqA.Choices))
            {
                _context.Entry(item).State = EntityState.Unchanged;
            }
            _context.Update(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            await _context.QuizAttempts.AddAsync(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }
        public async Task<List<QuizAttempt>> GetQuizAttemptsForQuiz(int quizId, string userId)
        {
            return await _context.QuizAttempts.Where(qA => qA.QuizId == quizId && qA.UserId == userId).ToListAsync();
        }
    }
}
