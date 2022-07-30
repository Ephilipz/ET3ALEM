using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessEntities.Enumerators;
using BusinessEntities.Models.Interfaces;
using Helpers.Extensions;
using Validation.CustomValidationAttributes;

namespace BusinessEntities.Models
{
    public class MultipleChoiceQuestion : Question, IParentEntity
    {
        public MultipleChoiceQuestion()
        {
            QuestionType = QuestionType.MCQ;
            Choices = new List<Choice>();
        }

        public McqAnswerType McqAnswerType { get; set; } = McqAnswerType.SingleChoice;

        [MinimumListLength(2)] public virtual List<Choice> Choices { get; set; }

        public IEnumerable<IEnumerable<object>> GetChildEntitiesToDelete()
        {
            return new List<IEnumerable<object>>
            {
                Choices.GetDeletedElements()
            };
        }

        public IEnumerable<IEnumerable<object>> GetChildEntitiesToAdd()
        {
            return new List<IEnumerable<object>>
            {
                Choices.GetAddedElements()
            };
        }

        public IEnumerable<IEnumerable<object>> GetChildEntitiesToUpdate()
        {
            return new List<IEnumerable<object>>
            {
                Choices.GetUpdatedElements()
            };
        }

        public IEnumerable<IEnumerable<object>> GetAllChildEntities()
        {
            return new List<IEnumerable<object>> {Choices};
        }

        public void SetChildEntitiesToNull()
        {
            Choices = null;
        }

        public IEnumerable<IEnumerable<object>> RemoveDeletedEntitiesFromChildren()
        {
            var itemsToDelete = Choices.GetDeletedElements().ToList();
            Choices.RemoveAll(choice => choice.Id < 0);
            foreach (var choice in itemsToDelete)
            {
                choice.Id = Math.Abs(choice.Id);
            }

            return new List<IEnumerable<object>> {itemsToDelete};
        }
    }
}