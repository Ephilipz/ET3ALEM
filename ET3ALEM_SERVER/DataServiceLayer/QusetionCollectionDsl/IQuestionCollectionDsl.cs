using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataServiceLayer
{
    public interface IQuestionCollectionDsl
    {
        Task<List<QuestionCollection>> GetQuestionCollections(string userId);
        Task<QuestionCollection> GetQuestionCollection(int id, string userId);
        Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection);
        Task PutQuestionCollection(int id, QuestionCollection collection);
        Task<QuestionCollection> DeleteQuestionCollection(int id);
        Task<bool> NameExists(string name);
    }
}