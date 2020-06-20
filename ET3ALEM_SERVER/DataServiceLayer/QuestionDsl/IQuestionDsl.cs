using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataServiceLayer.QuestionDataService
{
    public  interface IQuestionDsl
    {
        Task<List<Question>> GetQuestions();
        Task<Question> InsertQuestion(Question question);
    }
}
