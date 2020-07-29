using System;
using System.Collections.Generic;
using System.Text;
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
        [MinimumListLength(minNumberofElements: 2)]
        public List<Choice> Choices { get; set; }
    }
}
