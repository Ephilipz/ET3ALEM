using System;
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

        public async Task<IEnumerable<Question>> InsertQuestions(IEnumerable<Question> questions)
        {
            var insertQuestions = questions.ToList();
            _context.Questions.AddRange(insertQuestions);
            await _context.SaveChangesAsync();
            return insertQuestions;
        }

        public async Task<IEnumerable<Question>> DeleteQuestions(IEnumerable<int> questionIds)
        {
            var questions = _context.Questions.Where(q => questionIds.Contains(q.Id));
            _context.Questions.RemoveRange(questions);
            foreach (var question in questions)
            {
                RemoveChildEntities(question);
            }

            await _context.SaveChangesAsync();
            return questions;
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
                foreach (var childEntity in childEntityCollection)
                {
                    _context.Entry(childEntity).State = EntityState.Deleted;
                }
            }
        }

        public async Task PutQuestion(Question question)
        {
            UpdateChildEntities(question);
            RemoveOldData(question);
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> PutQuestions(IEnumerable<Question> questions)
        {
            questions = questions.ToList();
            foreach (var question in questions)
            {
                UpdateChildEntities(question);
                RemoveOldData(question);
                _context.Entry(question).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return questions;
        }

        private void UpdateChildEntities(Question question)
        {
            if (question is not IParentEntity parentEntity)
            {
                return;
            }

            var itemsToDelete = parentEntity.RemoveDeletedEntitiesFromChildren();

            SetItemsState(itemsToDelete, EntityState.Deleted);

            SetItemsState(parentEntity.GetChildEntitiesToAdd(), EntityState.Added);
            SetItemsState(parentEntity.GetChildEntitiesToUpdate(), EntityState.Modified);

            //This is to avoid updating the child entities twice
            // parentEntity.SetChildEntitiesToNull();
        }

        private void SetItemsState(IEnumerable<IEnumerable<object>> items, EntityState state)
        {
            foreach (var nestedItemsList in items)
            {
                var itemList = nestedItemsList.ToList();
                foreach (var item in itemList)
                {
                    _context.Entry(item).State = state;
                }
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