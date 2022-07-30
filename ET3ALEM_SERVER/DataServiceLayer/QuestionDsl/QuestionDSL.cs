using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.Models.Interfaces;
using DataAccessLayer;
using Helpers.Extensions;
using Microsoft.AspNetCore.Mvc.Diagnostics;

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

        public async Task<IEnumerable<Question>> InsertQuestions(IEnumerable<Question> questions)
        {
            var questionsList = questions.ToList();
            await _IQuestionDal.InsertQuestions(questionsList);
            await PreformAfterSaveAction(questionsList);

            return questionsList;
        }

        private async Task PreformAfterSaveAction(IReadOnlyCollection<Question> questions)
        {
            var afterSaveQuestions = questions.Where(q => q is IAfterSaveAction).ToList();
            if (afterSaveQuestions.Count == 0)
            {
                return;
            }
            
            foreach (var question in afterSaveQuestions)
            {
                (question as IAfterSaveAction).PreformAfterSaveAction();
            }

            await _IQuestionDal.PutQuestions(afterSaveQuestions);
        }

        public async Task<IEnumerable<Question>> PutQuestions(IEnumerable<Question> questions)
        {
            var questionsList = questions.ToList();
            await _IQuestionDal.PutQuestions(questionsList);
            await PreformAfterSaveAction(questionsList);
            return questionsList;
        }

        public async Task<IEnumerable<Question>> DeleteQuestions(IEnumerable<int> questionIds)
        {
            return await _IQuestionDal.DeleteQuestions(questionIds);
        }

        public async Task UpdateQuestionsBasedOnId(IEnumerable<Question> questions)
        {
            await InsertQuestions(questions.GetAddedElements().ToList());
            await DeleteQuestions(questions.GetDeletedElements().Select(q => q.Id * -1).ToList());
            await PutQuestions(questions.GetUpdatedElements().ToList());
        }
    }
}