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
        [MinimumListLength(minNumberofElements: 1)]
        public virtual List<Choice> Choices { get; set; }
        public override double GradeQuestion()
        {
            IsGraded = true;
            MultipleChoiceQuestion mcq = QuizQuestion.Question as MultipleChoiceQuestion;
            double grade = Choices.Count(choice => mcq.Choices.Where(choice => choice.IsAnswer).Any(rightAnswer => rightAnswer.Id == choice.Id)) / (double)mcq.Choices.Count(choice => choice.IsAnswer);
            return Math.Round(grade * QuizQuestion.Grade, 2);
        }
    }
}
