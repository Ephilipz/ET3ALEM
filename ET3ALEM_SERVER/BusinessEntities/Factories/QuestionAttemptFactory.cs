using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using System;

namespace BusinessEntities.Factories
{
    public class QuestionAttemptFactory
    {
        public static QuestionAttempt GetQuestionAttemptFromQuizQuestion(
            QuizQuestion quizQuestion, Action<QuestionAttempt> configure = null)
        {
            var questionAttempt =
                GetQuestionAttemptFromQuestionType(quizQuestion.Question
                    .QuestionType);
            configure?.Invoke(questionAttempt);
            return questionAttempt;
        }

        public static QuestionAttempt GetQuestionAttemptFromQuestionType
            (QuestionType type)
        {
            return type switch
            {
                QuestionType.MCQ => new MCQAttmept(),
                QuestionType.TrueFalse => new TrueFalseAttempt(),
                QuestionType.ShortAnswer => new ShortAnswerAttempt(),
                QuestionType.LongAnswer => new LongAnswerAttempt(),
                QuestionType.Order => new OrderAttempt(),
                _ => throw new InvalidCastException(
                    "No valid question type was provided")
            };
        }
    }
}