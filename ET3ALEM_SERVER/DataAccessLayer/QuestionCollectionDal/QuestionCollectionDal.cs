using BusinessEntities.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class QuestionCollectionDal : IQuestionCollectionDal
    {
        private readonly ApplicationContext _context;
        public QuestionCollectionDal(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<QuestionCollection> GetQuestionCollection(string userId)
        {
            var collection  = await _context.QuestionCollections.FirstAsync(collection => collection.UserId == userId);
            collection.TrueFalseQuestions = _context.Questions.OfType<TrueFalseQuestion>().Where(q => q.QuestionCollectionId == collection.Id).ToList();
            collection.MultipleChoiceQuestions  = _context.Questions.OfType<MultipleChoiceQuestion>().Where(q => q.QuestionCollectionId == collection.Id).Include(mcq=>mcq.Choices).ToList();
            return collection;
        }

        public async Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection)
        {
            await _context.QuestionCollections.AddAsync(questionCollection);
            await _context.SaveChangesAsync();
            return questionCollection;
        }
    }
}
