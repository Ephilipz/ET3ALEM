using System.Collections.Generic;
using BusinessEntities.Enumerators;
using Validation.CustomValidationAttributes;

namespace BusinessEntities.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion()
        {
            QuestionType = QuestionType.MCQ;
            Choices = new List<Choice>();
        }

        public McqAnswerType McqAnswerType { get; set; } = McqAnswerType.SingleChoice;

        [MinimumListLength(2)] public virtual List<Choice> Choices { get; set; }
    }
}