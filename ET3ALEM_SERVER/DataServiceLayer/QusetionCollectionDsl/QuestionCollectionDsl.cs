using BusinessEntities.Models;
using DataAccessLayer;
using DataServiceLayer;
using System;
using System.Collections.Generic;
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
            await _IQuestionCollectionDal.InsertQuestionCollection(questionCollection);
            return questionCollection;
        }

        public async Task PutQuestionCollection(QuestionCollection questionCollection)
        {
            await _IQuestionCollectionDal.PutQuestionCollection(questionCollection);
        }

        public Task<QuestionCollection> DeleteQuestionCollection(int id)
        {
            return _IQuestionCollectionDal.DeleteQuestionCollection(id);
        }
    }
}
