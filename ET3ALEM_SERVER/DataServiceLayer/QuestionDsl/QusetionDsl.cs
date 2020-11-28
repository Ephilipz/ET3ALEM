using DataAccessLayer;
using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class QuestionDsl : IQuestionDsl
    {
        private readonly IQuestionDal _IQuestionDal;
        public QuestionDsl(IQuestionDal QuestionDal)
        {
            _IQuestionDal = QuestionDal;
        }


        public Task<List<Question>> GetQuestions()
        {
            return _IQuestionDal.GetQuestions();
        }

        public Task<Question> InsertQuestion(Question question)
        {
            return  _IQuestionDal.InsertQuestion(question);
        }
        public Task<Question> DeleteQuestion(int questionId)
        {
            return _IQuestionDal.DeleteQuestion(questionId);
        }

        public Task PutQuestion(Question question)
        {
            return _IQuestionDal.PutQuestion(question);
        }
    }
}
