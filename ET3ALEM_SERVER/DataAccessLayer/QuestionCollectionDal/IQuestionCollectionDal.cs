using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IQuestionCollectionDal
    {
        Task<QuestionCollection> GetQuestionCollection(string UserId);
        Task<QuestionCollection> InsertQuestionCollection(QuestionCollection questionCollection);
    }
}
