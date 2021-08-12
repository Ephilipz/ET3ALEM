using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class ShortAnswerAttempt : QuestionAttempt
    {
        public string? Answer { get; set; }
        public override void GradeQuestion()
        {
            ShortAnswerQuestion question = QuizQuestion.Question as ShortAnswerQuestion;

            if (string.IsNullOrWhiteSpace(question.PossibleAnswers))
            {
                IsGraded = false;
                return;
            }

            IsGraded = true;
            foreach (string possibleAnswer in question.PossibleAnswers.Split(','))
            {
                //check if the answers match the possible answer
                bool isMatching = Answer.Trim().Equals(possibleAnswer.Trim(), question.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase);

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
