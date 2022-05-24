using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.Models.Interfaces;
using DataAccessLayer;

namespace DataServiceLayer
{
    public class QuestionDsl : IQuestionDsl
    {
        private readonly IQuestionDal _IQuestionDal;

        public QuestionDsl(IQuestionDal questionDal)
        {
            _IQuestionDal = questionDal;
        }


        public Task<List<Question>> GetQuestions()
        {
            return _IQuestionDal.GetQuestions();
        }

        public async Task<Question> InsertQuestion(Question question)
        {
            var returnedQuestion = await _IQuestionDal.InsertQuestion(question);
            await PreformAfterSaveAction(question);

            return returnedQuestion;
        }

        private async Task PreformAfterSaveAction(Question question)
        {
            if (question is not IAfterSaveAction afterSaveAction)
            {
                return;
            }

            afterSaveAction.PreformAfterSaveAction();
            await _IQuestionDal.PutQuestion(question);
        }

        public Task<Question> DeleteQuestion(int questionId)
        {
            return _IQuestionDal.DeleteQuestion(questionId);
        }

        public async Task PutQuestion(Question question)
        {
            await _IQuestionDal.PutQuestion(question);
            await PreformAfterSaveAction(question);
        }
    }
}