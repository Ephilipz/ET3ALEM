using BusinessEntities.Models;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }

            return quiz;
        }

        public async Task<Quiz> GetQuiz(int quizId)
        {
            Quiz quiz = await _context.Quizzes.Where(q => q.Id == quizId)
                .Include(quiz => quiz.QuizQuestions)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question => ((MultipleChoiceQuestion)question).Choices).FirstAsync();

            return quiz;
        }

        public async Task<string> GetQuizTitleFromCode(string code)
        {
            Quiz quiz = await _context.Quizzes.FirstAsync(q => q.code.ToUpper() == code.ToUpper());
            if (quiz != null)
                return quiz.Name;
            return null;
        }

        public async Task<Quiz> GetSimpleQuiz(int quizId)
        {
            return await _context.FindAsync<Quiz>(quizId);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz> InsertQuiz(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            quiz.code = QuizHelper.getCode(quiz.Id);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task PutQuiz(int id, Quiz quiz)
        {
            foreach (QuizQuestion quizQuestion in quiz.QuizQuestions)
            {
                if (quizQuestion.Id == 0)
                {
                    await _context.QuizQuestions.AddAsync(quizQuestion);
                }
            }
            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
