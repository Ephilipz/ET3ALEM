using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IQuestionCollectionDsl
    {
        Task<List<QuestionCollection>> GetQuestionCollections(string userId);
        Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection);
    }
}
