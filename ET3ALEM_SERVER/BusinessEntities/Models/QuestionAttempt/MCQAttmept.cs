using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.CustomValidationAttributes;

namespace BusinessEntities.Models
{
    public class MCQAttmept : QuestionAttempt
    {
        public virtual List<Choice>? Choices { get; set; }
        public override void GradeQuestion()
        {
            MultipleChoiceQuestion mcq = QuizQuestion.Question as MultipleChoiceQuestion;
            //if no right answer exists mark the question as ungraded
            if (!mcq.Choices.Any(choice => choice.IsAnswer))
                return;
            IsGraded = true;
            //if the user did not choose an answer give them 0
            if (Choices == null || Choices.Count == 0)
            {
                this.Grade = 0;
                return;
            }
            double incorrectChoices = mcq.Choices.Count(choice => !choice.IsAnswer && Choices.Any(selectedChoice => selectedChoice.Id == choice.Id));
            double correctChoices = Choices.Count(choice => mcq.Choices.Where(choice => choice.IsAnswer).Any(rightAnswer => rightAnswer.Id == choice.Id));
            double grade = (correctChoices - incorrectChoices) / (double)mcq.Choices.Count(choice => choice.IsAnswer);
            this.Grade = Math.Max(Math.Round(grade * QuizQuestion.Grade, 2), 0);
        }
    }
}
