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
            return await _context.Quizzes.FindAsync(quizId);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz> InsertQuiz(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task PutQuiz(int id, Quiz quiz)
        {
            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
