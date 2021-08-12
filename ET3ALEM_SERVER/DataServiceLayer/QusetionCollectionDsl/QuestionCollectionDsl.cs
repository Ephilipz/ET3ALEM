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

        public async Task<QuestionCollection> DeleteQuestionCollection(int id)
        {
            QuestionCollection collection = await _IQuestionCollectionDal.DeleteQuestionCollection(id);
            foreach(Question question in collection.Questions)
            {
                await _IQuestionDsl.DeleteQuestion(question.Id);
            }
            return collection;
        }

        public Task<bool> NameExists(string name)
        {
            return _IQuestionCollectionDal.NameExists(name);
        }
    }
}
