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
            return _context.QuizAttempts.Where(qA => qA.Id == id).Include(qA => qA.QuestionsAttempts).AsSplitQuery().AsNoTracking().FirstAsync();
        }

        public Task<QuizAttempt> GetQuizAttemptWithQuiz(int id)
        {
            return _context.QuizAttempts.Where(qA => qA.Id == id)
                .Include(qA => qA.Quiz)
                .Include(qA => qA.QuestionsAttempts)
                .ThenInclude(qAS => ((MCQAttmept)qAS).Choices)
                .Include(qA => qA.QuestionsAttempts.OrderBy(qAs => qAs.Sequence))
                .ThenInclude(qAS => qAS.QuizQuestion)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question => ((MultipleChoiceQuestion)question).Choices).AsSplitQuery().AsNoTracking().FirstAsync();
        }

        public async Task<QuizAttempt> PutQuizAttempt(int id, QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            quizAttempt.Quiz = null;
            foreach (var item in quizAttempt.QuestionsAttempts.OfType<MCQAttmept>().SelectMany(mcqA => mcqA.Choices))
            {
                _context.Entry(item).State = EntityState.Unchanged;
            }
            _context.Update(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            foreach (QuestionAttempt qA in quizAttempt.QuestionsAttempts)
            {
                _context.Entry(qA.QuizQuestion).State = EntityState.Unchanged;
            }
            await _context.QuizAttempts.AddAsync(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<QuizAttempt> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {

            //detach child entities that will not be tracked
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            quizAttempt.QuestionsAttempts.OfType<MCQAttmept>().ToList().ForEach(mcqA => mcqA.Choices = null);
            quizAttempt.Quiz = null;

            _context.Update(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(int quizId, string userId)
        {
            return await _context.QuizAttempts.Where(qA => qA.QuizId == quizId && qA.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetAllQuizAttemptsForQuiz(int quizId)
        {
            return await _context.QuizAttempts.Where(qA => qA.QuizId == quizId)
                .Include(qA => qA.Quiz).Include(qA => qA.User).AsSplitQuery().AsNoTracking().ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetQuizAttempts(string userId)
        {
            return await _context.QuizAttempts.Where(qA => qA.UserId == userId).Include(qA => qA.Quiz).OrderByDescending(qA => qA.SubmitTime).AsNoTracking().ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetUngradedQuizAttempts(string userId)
        {
            return await _context.QuizAttempts.Where(quizAttempt => !quizAttempt.IsGraded && quizAttempt.Quiz.UserId == userId).Include(qA => qA.User).AsSplitQuery().AsNoTracking().ToListAsync();
        }

        public async Task DeleteRelatedQuizAttempts(int quizId)
        {
            IEnumerable<QuizAttempt> attempts = _context.QuizAttempts.Where(qA => qA.QuizId == quizId).Include(qA => qA.QuestionsAttempts);
            foreach (QuizAttempt attempt in attempts)
            {
                _context.Remove(attempt);
                _context.RemoveRange(attempt.QuestionsAttempts);
                _context.RemoveRange(attempt.QuestionsAttempts.Select(qA => ((MCQAttmept)qA).Choices));
            }
            await _context.SaveChangesAsync();
        }
    }
}
