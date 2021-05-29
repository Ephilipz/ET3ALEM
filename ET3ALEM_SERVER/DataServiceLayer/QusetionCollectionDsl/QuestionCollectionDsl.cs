using BusinessEntities.Models;
using DataAccessLayer;
using DataServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class QuestionCollectionDsl : IQuestionCollectionDsl
    {
        private readonly IQuestionCollectionDal _IQuestionCollectionDal;
        public QuestionCollectionDsl(IQuestionCollectionDal IQuestionCollectionDal)
        {
            _IQuestionCollectionDal = IQuestionCollectionDal;
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
            await _IQuestionCollectionDal.InsertQuestionCollection(questionCollection);
            return questionCollection;
        }

        public async Task PutQuestionCollection(int id, QuestionCollection updatedCollection)
        {
            var collection = await _IQuestionCollectionDal.GetQuestionCollection(id, updatedCollection.UserId);
            collection.Name = updatedCollection.Name;
            collection.Questions = updatedCollection.Questions;
            collection.Questions.OfType<MultipleChoiceQuestion>().ToList().ForEach(mcq => mcq.Choices.ForEach(choice=>choice.Id = 0));
            await _IQuestionCollectionDal.PutQuestionCollection(collection);
        }

        public Task<QuestionCollection> DeleteQuestionCollection(int id)
        {
            return _IQuestionCollectionDal.DeleteQuestionCollection(id);
        }
    }
}
