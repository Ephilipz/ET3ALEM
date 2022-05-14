using System;
using BusinessEntities.Enumerators;
using BusinessEntities.Models;

namespace BusinessEntities.Factories
{
    public class QuestionFactory
    {
        public static Question GetQuestionFromQuestionType
            (QuestionType type)
        {
            return type switch
            {
                QuestionType.MCQ => new MultipleChoiceQuestion(),
                QuestionType.TrueFalse => new TrueFalseQuestion(),
                QuestionType.ShortAnswer => new ShortAnswerQuestion(),
                QuestionType.LongAnswer => new LongAnswerQuestion(),
                QuestionType.Order => new OrderQuestion(),
                _ => throw new InvalidCastException(
                    "No valid question type was provided")
            };
        }
    }
}