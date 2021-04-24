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
        public virtual List<Choice> Choices { get; set; }
        public override void GradeQuestion()
        {
            IsGraded = true;
            MultipleChoiceQuestion mcq = QuizQuestion.Question as MultipleChoiceQuestion;
            double grade = Choices.Count(choice => mcq.Choices.Where(choice => choice.IsAnswer).Any(rightAnswer => rightAnswer.Id == choice.Id)) / (double)mcq.Choices.Count(choice => choice.IsAnswer);
            this.Grade = Math.Round(grade * QuizQuestion.Grade, 2);
        }
    }
}
