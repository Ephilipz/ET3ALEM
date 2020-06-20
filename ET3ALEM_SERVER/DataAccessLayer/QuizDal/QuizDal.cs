using BusinessEntities.Models;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QuizDal
{
    public class QuizDal : IQuizDal
    {
        private readonly ApplicationContext _context;
        public QuizDal(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Quiz> GetQuiz(int quizId)
        {
            return await _context.Quizzes.FindAsync(quizId);
        }

        public async Task<Quiz> InsertQuiz(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }
    }
}
