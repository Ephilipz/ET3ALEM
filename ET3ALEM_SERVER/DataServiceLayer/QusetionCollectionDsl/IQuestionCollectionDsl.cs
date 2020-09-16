using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public interface IQuestionCollectionDsl
    {
        Task<QuestionCollection> GetQuestionCollection(string userId);
        Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection);
    }
}
