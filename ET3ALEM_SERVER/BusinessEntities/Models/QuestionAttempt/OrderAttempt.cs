using System;
using System.Collections.Generic;

namespace BusinessEntities.Models
{
    public class OrderAttempt : QuestionAttempt
    {
        public string Answer { get; set; }
        public override void GradeQuestion()
        {
            var attemptAnswerList = Answer.Split(',');
            var correctAnswerList = ((OrderQuestion) QuizQuestion.Question)?
                .CorrectOrderIds?.Split(',');
            if (correctAnswerList == null)
            {
                return;
            }

            if (correctAnswerList.Length != attemptAnswerList.Length)
            {
                throw new MissingFieldException(
                    "Correct Order must have the same length as Attempt Order elements");
            }

            var outOfOrderCount = GetOutOfOrderCount(attemptAnswerList, correctAnswerList);

            double pointsDeducted = outOfOrderCount * (QuizQuestion.Grade * 1.0 / attemptAnswerList.Length);
            Grade = QuizQuestion.Grade - pointsDeducted;
            IsGraded = true;
        }

        private static int GetOutOfOrderCount(IReadOnlyList<string> attemptAnswerList,
            IReadOnlyList<string> correctAnswerList)
        {
            var outOfOrderCount = 0;
            for (var i = 0; i < attemptAnswerList.Count; ++i)
            {
                if (attemptAnswerList[i].Trim() != correctAnswerList[i].Trim())
                {
                    outOfOrderCount++;
                }
            }

            return outOfOrderCount;
        }
    }
}