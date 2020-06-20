using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QuestionDataAccess
{
    public class QuestionDal : IQuestionDal
    {
        private readonly ApplicationContext _context;
        public QuestionDal(ApplicationContext context)
        {
            _context = context;
        }
        public Task<List<Question>> GetQuestions()
        {
            return _context.Questions.ToListAsync();
        }
        public async Task<Question> InsertQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            return question;
        }
    }
}
