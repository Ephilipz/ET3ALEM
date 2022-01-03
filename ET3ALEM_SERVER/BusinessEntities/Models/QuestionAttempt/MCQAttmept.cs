using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities.Models
{
    public class MCQAttmept : QuestionAttempt
    {
#nullable enable
        public virtual List<Choice>? Choices { get; set; }

        public override void GradeQuestion()
        {
            if (QuizQuestion.Question == null)
            {
                return;
            }

            var mcq = QuizQuestion.Question as MultipleChoiceQuestion;

            if (mcq == null || NoRightAnswerExists(mcq))
            {
                return;
            }

            IsGraded = true;
            if (NoChoiceSelected())
            {
                Grade = 0;
                return;
            }

            double incorrectChoices = mcq.Choices.Count(choice =>
                !choice.IsAnswer && Choices.Any(selectedChoice =>
                    selectedChoice.Id == choice.Id));
            double correctChoices = Choices.Count(choice => mcq.Choices
                .Where(c => c.IsAnswer)
                .Any(rightAnswer => rightAnswer.Id == choice.Id));
            var grade = (correctChoices - incorrectChoices) /
                        mcq.Choices.Count(choice => choice.IsAnswer);
            Grade = Math.Max(Math.Round(grade * QuizQuestion.Grade, 2), 0);
        }

        private bool NoChoiceSelected()
        {
            return Choices == null || Choices.Count == 0;
        }

        private static bool NoRightAnswerExists(MultipleChoiceQuestion mcq)
        {
            return !mcq.Choices.Any(choice => choice.IsAnswer);
        }
    }
}