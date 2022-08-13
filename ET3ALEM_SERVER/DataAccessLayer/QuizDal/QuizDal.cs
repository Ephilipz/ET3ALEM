using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

namespace DataAccessLayer
{
    public class QuizDal : IQuizDal
    {
        private readonly ApplicationContext _context;
        private readonly IQuizHelper _quizHelper;

        public QuizDal(ApplicationContext context, IQuizHelper quizHelper)
        {
            _context = context;
            _quizHelper = quizHelper;
        }

        public async Task<Quiz> DeleteQuiz(int id)
        {
            var quiz = await GetQuiz(id);

            if (quiz == null) return null;
            
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return quiz;
        }

        public async Task<Quiz> GetQuiz(int quizId)
        {
            var quiz = await _context.Quizzes.Where(q => q.Id == quizId)
                .Include(quiz => quiz.QuizQuestions)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question =>
                    ((MultipleChoiceQuestion) question).Choices)
                .Include(quiz => quiz.QuizQuestions)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question =>
                    ((OrderQuestion) question).OrderedElements)
                .AsNoTracking().FirstAsync();
            return quiz;
        }

        public async Task<string> GetQuizTitleFromCode(string code)
        {
            var quiz = await _context.Quizzes.AsNoTracking().FirstOrDefaultAsync(q => q.Code.ToUpper() == code.ToUpper()); 
            return quiz?.Name;
        }

        public async Task<Quiz> GetSimpleQuiz(int quizId)
        {
            return await _context.FindAsync<Quiz>(quizId);
        }

        public async Task<List<Quiz>> GetQuizzes(string userId)
        {
            return await _context.Quizzes
                .Where(quiz => string.Equals(userId, quiz.UserId))
                .OrderByDescending(quiz => quiz.CreatedDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Quiz> InsertQuiz(Quiz quiz)
        {
            if (quiz.QuizQuestions.Count != 0)
            {
                quiz.TotalGrade = quiz.QuizQuestions.Sum(question => question.Grade);
            }
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            quiz.Code = _quizHelper.GetCode(quiz.Id);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task PutQuiz(Quiz quiz)
        {
            foreach (var quizQuestion in quiz.QuizQuestions)
            {
                switch (quizQuestion.Id)
                {
                    case 0:
                        _context.QuizQuestions.Add(quizQuestion);
                        break;
                    case < 0:
                        quizQuestion.Id *= -1;
                        _context.QuizQuestions.Remove(quizQuestion);
                        break;
                    default:
                        _context.Entry(quizQuestion).State = EntityState.Modified;
                        break;
                }
            }

            quiz.TotalGrade = quiz.QuizQuestions.Sum(question => question.Grade);
            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task<Quiz> GetBasicQuizByCode(string code)
        {
            return _context.Quizzes
                .Where(q => q.Code.ToLower() == code.ToLower())
                .Include(q => q.QuizQuestions)
                .AsNoTracking().FirstAsync();
        }

        public Task<Quiz> GetFullQuizByCode(string code)
        {
            var id = _context.Quizzes.FirstOrDefault(q => q.Code.ToLower() == code)?.Id;
            if (!id.HasValue)
            {
                return null;
            }
            return GetQuiz(id.Value);
        }

        public async Task<List<QuizAttempt>> GetUngradedQuizzesForUser(string userId)
        {
            var ungradedAttempts = await _context.QuizAttempts
                .Where(attempt => !attempt.IsGraded && attempt.Quiz.UserId == userId)
                .Include(attempt => attempt.Quiz)
                .Include(attempt => attempt.User)
                .OrderByDescending(attempt => attempt.SubmitTime)
                .ToListAsync();
            return ungradedAttempts;
        }
    }
}