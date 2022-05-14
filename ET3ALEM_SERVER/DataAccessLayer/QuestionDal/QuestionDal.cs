using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using BusinessEntities.Models.Interfaces;
using DataAccessLayer.Extensions;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

namespace DataAccessLayer
{
    public class QuestionDal : IQuestionDal
    {
        private readonly ApplicationContext _context;

        public QuestionDal(ApplicationContext context)
        {
            _context = context;
        }

        public Task<List<Question>> GetQuestions()
        {
            return _context.Questions.ToListAsync();
        }

        public async Task<Question> InsertQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<Question> DeleteQuestion(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null) return null;

            _context.Questions.Remove(question);

            RemoveChildEntities(question);

            await _context.SaveChangesAsync();

            return question;
        }

        private void RemoveChildEntities(Question question)
        {
            if (question is not IParentEntity parentEntity)
            {
                return;
            }

            foreach (var childEntityCollection in parentEntity.GetAllChildEntities())
            {
                _context.RemoveRange(childEntityCollection);
            }
        }

        public async Task PutQuestion(Question question)
        {
            UpdateChildEntities(question);
            RemoveOldData(question);
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private void UpdateChildEntities(Question question)
        {
            if (question is not IParentEntity parentEntity)
            {
                return;
            }

            foreach (var deleteCollection in parentEntity.GetChildEntitiesToDelete())
            {
                _context.RemoveRange(deleteCollection);
            }

            foreach (var addCollection in parentEntity.GetChildEntitiesToAdd())
            {
                _context.AddRange(addCollection);
            }

            foreach (var updateCollection in parentEntity.GetChildEntitiesToUpdate())
            {
                _context.UpdateRange(updateCollection);
            }
        }

        private void RemoveOldData(Question question)
        {
            var includedChildEntities = _context.GetIncludePaths(typeof(Question));
            
            var oldQuestion = _context.Questions
                .AsNoTracking()
                .Where(q => q.Id == question.Id)
                .Include(includedChildEntities)
                .First();

            var isQuestionTypeChanged = oldQuestion.QuestionType != question.QuestionType;
            if (isQuestionTypeChanged)
            {
                RemoveChildEntities(oldQuestion);
            }
        }
    }
}