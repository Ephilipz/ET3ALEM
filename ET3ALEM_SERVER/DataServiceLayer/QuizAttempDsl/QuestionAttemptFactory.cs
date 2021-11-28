using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    internal class QuestionAttemptFactory
    {
        public static QuestionAttempt GetQuestionAttemptFromQuizQuestion(QuizQuestion quizQuestion, Action<QuestionAttempt> configure = null)
        {
            QuestionAttempt questionAttempt;
            switch (quizQuestion.Question.QuestionType)
            {
                case QuestionType.MCQ:
                    questionAttempt = new MCQAttmept();
                    break;
                case QuestionType.TrueFalse:
                    questionAttempt = new TrueFalseAttempt();
                    break;
                case QuestionType.ShortAnswer:
                    questionAttempt = new ShortAnswerAttempt();
                    break;
                case QuestionType.LongAnswer:
                    questionAttempt = new LongAnswerAttempt();
                    break;
                default:
                    throw new InvalidCastException("No valid question type was provided");
            }
            configure?.Invoke(questionAttempt);
            return questionAttempt;
        }
    }
}
