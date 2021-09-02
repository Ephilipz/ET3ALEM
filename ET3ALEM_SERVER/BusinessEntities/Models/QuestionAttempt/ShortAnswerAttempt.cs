#nullable enable
using System;

namespace BusinessEntities.Models
{
    public class ShortAnswerAttempt : QuestionAttempt
    {
        public string? Answer { get; set; }

        public override void GradeQuestion()
        {
            var question = QuizQuestion.Question as ShortAnswerQuestion;

            if (string.IsNullOrWhiteSpace(question?.PossibleAnswers))
            {
                IsGraded = false;
                return;
            }

            IsGraded = true;
            foreach (string possibleAnswer in question.PossibleAnswers.Split(','))
            {
                var compareType = question.CaseSensitive
                    ? StringComparison.InvariantCulture
                    : StringComparison.InvariantCultureIgnoreCase;
                //check if the answers match the possible answer
                var isMatching = Answer != null && Answer.Trim()
                    .Equals(possibleAnswer.Trim(), compareType);

                //if it's matching, give full grade and return
                if (isMatching)
                {
                    Grade = QuizQuestion.Grade;
                    return;
                }
            }

            //if none match set Grade to zero
            Grade = 0;
        }
    }
}