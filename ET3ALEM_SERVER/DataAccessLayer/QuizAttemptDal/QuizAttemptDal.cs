using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

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
            return _context.QuizAttempts.Where(qA => qA.Id == id)
                .Include(qA => qA.QuestionsAttempts).AsSplitQuery()
                .AsNoTracking().FirstAsync();
        }

        public Task<QuizAttempt> GetQuizAttemptWithQuiz(int id)
        {
            return _context.QuizAttempts.Where(qA => qA.Id == id)
                .Include(qA => qA.Quiz)
                .Include(qA => qA.QuestionsAttempts)
                .ThenInclude(qAS => ((MCQAttmept) qAS).Choices)
                .Include(qA => qA.QuestionsAttempts)
                .ThenInclude(qAS => ((LongAnswerAttempt) qAS).LongAnswer)
                .Include(
                    qA => qA.QuestionsAttempts.OrderBy(qAs => qAs.Sequence))
                .ThenInclude(qAS => qAS.QuizQuestion)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question =>
                    ((MultipleChoiceQuestion) question).Choices)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task<QuizAttempt> PutQuizAttempt(QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            quizAttempt.Quiz = null;
            var choices = quizAttempt.QuestionsAttempts
                .OfType<MCQAttmept>().SelectMany(mcqA => mcqA.Choices);
            // foreach (var item in choices)
            // {
            //     _context.Entry(item).State = EntityState.Unchanged;
            // }
            _context.Update(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<QuizAttempt> PostQuizAttempt(
            QuizAttempt quizAttempt)
        {
            foreach (var qA in quizAttempt.QuestionsAttempts)
            {
                _context.Entry(qA.QuizQuestion).State = EntityState.Unchanged;
            }

            await _context.QuizAttempts.AddAsync(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<QuizAttempt> UpdateQuizAttemptGrade(
            QuizAttempt quizAttempt)
        {
            
            //detach child entities that will not be tracked
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            quizAttempt.QuestionsAttempts.OfType<MCQAttmept>().ToList()
                .ForEach(mcqA => mcqA.Choices = null);
            quizAttempt.Quiz = null;
            
            _context.Update(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(
            int quizId, string userId)
        {
            return await _context.QuizAttempts
                .Where(qA => qA.QuizId == quizId && qA.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetAllQuizAttemptsForQuiz(
            int quizId)
        {
            List<QuizAttempt> quizAttempts = await _context.QuizAttempts
                .Where(qA => qA.QuizId == quizId)
                .Include(qA => qA.Quiz)
                .Include(qA => qA.User)
                .OrderByDescending(qA => qA.Grade)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            return quizAttempts;
        }

        public async Task<List<QuizAttempt>> GetQuizAttempts(string userId)
        {
            return await _context.QuizAttempts
                .Where(qA => qA.UserId == userId)
                .Include(qA => qA.Quiz)
                .OrderByDescending(qA => qA.SubmitTime)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetUngradedAttemptsForQuiz(int quizId)
        {
            return await _context.QuizAttempts
                .Where(attempt => !attempt.IsGraded && attempt.QuizId == quizId)
                .Include(attempt => attempt.User)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}