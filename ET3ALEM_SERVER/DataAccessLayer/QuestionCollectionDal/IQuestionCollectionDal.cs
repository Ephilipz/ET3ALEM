using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataAccessLayer
{
    public interface IQuestionCollectionDal
    {
        Task<List<QuestionCollection>> GetQuestionCollections(string UserId);
        Task<QuestionCollection> GetQuestionCollection(int id, string UserId);
        Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection);
        Task PutQuestionCollection(QuestionCollection questionCollection);
        Task<QuestionCollection> DeleteQuestionCollection(int id);
        Task<bool> NameExists(string name);
    }
}