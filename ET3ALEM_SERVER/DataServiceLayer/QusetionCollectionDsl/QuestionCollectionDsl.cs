using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataAccessLayer;

namespace DataServiceLayer
{
    public class QuestionCollectionDsl : IQuestionCollectionDsl
    {
        private readonly IQuestionCollectionDal _IQuestionCollectionDal;
        private readonly IQuestionDsl _IQuestionDsl;

        public QuestionCollectionDsl(IQuestionCollectionDal IQuestionCollectionDal, IQuestionDsl IQuestionDsl)
        {
            _IQuestionCollectionDal = IQuestionCollectionDal;
            _IQuestionDsl = IQuestionDsl;
        }


        public async Task<List<QuestionCollection>> GetQuestionCollections(string userId)
        {
            return await _IQuestionCollectionDal.GetQuestionCollections(userId);
        }

        public async Task<QuestionCollection> GetQuestionCollection(int id, string userId)
        {
            return await _IQuestionCollectionDal.GetQuestionCollection(id, userId);
        }

        public async Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection)
        {
            questionCollection.CreatedDate = DateTime.UtcNow;
            await _IQuestionDsl.InsertQuestions(questionCollection.Questions);
            await _IQuestionCollectionDal.InsertQuestionCollection(questionCollection);
            return questionCollection;
        }

        public async Task PutQuestionCollection(int id, QuestionCollection collection)
        {
            await _IQuestionDsl.UpdateQuestionsBasedOnId(collection.Questions.ToList());
            await _IQuestionCollectionDal.PutQuestionCollection(collection);
        }

        public async Task<QuestionCollection> DeleteQuestionCollection(int id)
        {
            var collection = await _IQuestionCollectionDal.DeleteQuestionCollection(id);
            await _IQuestionDsl.DeleteQuestions(collection.Questions.Select(q => q.Id));
            return collection;
        }

        public Task<bool> NameExists(string name)
        {
            return _IQuestionCollectionDal.NameExists(name);
        }
    }
}