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

        public QuizDal(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Quiz> DeleteQuiz(int id)
        {
            var quiz = await GetQuiz(id);

            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }

            return quiz;
        }

        public async Task<Quiz> GetQuiz(int quizId)
        {
            var quiz = await _context.Quizzes.Where(q => q.Id == quizId)
                .Include(quiz => quiz.QuizQuestions)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question => ((MultipleChoiceQuestion) question).Choices).AsNoTracking().FirstAsync();

            return quiz;
        }

        public async Task<string> GetQuizTitleFromCode(string code)
        {
            var quiz = await _context.Quizzes.FirstAsync(q => q.Code.ToUpper() == code.ToUpper());
            if (quiz != null)
                return quiz.Name;
            return null;
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
                quiz.TotalGrade = quiz.QuizQuestions.Sum(question => question.Grade);
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            quiz.Code = QuizHelper.GetCode(quiz.Id);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task PutQuiz(Quiz quiz)
        {
            foreach (var quizQuestion in quiz.QuizQuestions)
                if (quizQuestion.Id == 0)
                {
                    await _context.QuizQuestions.AddAsync(quizQuestion);
                }
                else if (quizQuestion.Id < 0)
                {
                    quizQuestion.Id *= -1;
                    _context.QuizQuestions.Remove(quizQuestion);
                }
                else
                {
                    _context.Entry(quizQuestion).State = EntityState.Modified;
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
            var id = _context.Quizzes.First(q => q.Code.ToLower() == code)?.Id;
            if (id == null)
                return null;
            return GetQuiz((int) id);
        }

        public async Task<List<Quiz>> GetUngradedQuizzesForUser(string userId)
        {
            return await _context.Quizzes
                .Where(quiz => quiz.UserId == userId && quiz.QuizAttempts.Exists(qA => !qA.IsGraded))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}