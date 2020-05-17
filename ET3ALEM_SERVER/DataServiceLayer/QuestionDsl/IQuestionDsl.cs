using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Server_Application.BusinessEntities.Models;

namespace DataServiceLayer.QuestionDataService
{
    public  interface IQuestionDsl
    {
        Task<List<Question>> GetQuestions();
    }
}
