using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

namespace DataAccessLayer
{
    public class QuestionCollectionDal : IQuestionCollectionDal
    {
        private readonly ApplicationContext _context;

        public QuestionCollectionDal(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionCollection>> GetQuestionCollections(string userId)
        {
            var collections = await _context.QuestionCollections.Where(collection => collection.UserId == userId)
                .ToListAsync();
            return collections;
        }

        public async Task<QuestionCollection> GetQuestionCollection(int id, string userId)
        {
            return await _context.QuestionCollections
                .Where(collection => collection.Id == id && collection.UserId == userId)
                .Include(collection => collection.Questions)
                .ThenInclude(question => ((MultipleChoiceQuestion) question).Choices).FirstAsync();
        }

        public async Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection)
        {
            await _context.QuestionCollections.AddAsync(questionCollection);
            await _context.SaveChangesAsync();
            return questionCollection;
        }

        public async Task PutQuestionCollection(QuestionCollection collection)
        {
            _context.Update(collection);
            await _context.SaveChangesAsync();
        }

        public async Task<QuestionCollection> DeleteQuestionCollection(int id)
        {
            var questionCollection = await _context.QuestionCollections.Where(q => q.Id == id)
                .Include(collection => collection.Questions)
                .ThenInclude(question => ((MultipleChoiceQuestion) question).Choices).FirstAsync();
            if (questionCollection != null)
            {
                _context.QuestionCollections.Remove(questionCollection);
                await _context.SaveChangesAsync();
            }

            return questionCollection;
        }

        public Task<bool> NameExists(string name)
        {
            return _context.QuestionCollections.AnyAsync(collection =>
                collection.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}