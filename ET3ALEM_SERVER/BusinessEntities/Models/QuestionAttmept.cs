using BusinessEntities.CustomConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Validation.CustomValidationAttributes;

namespace BusinessEntities.Models
{
    [JsonConverter(typeof(QuestionAttemptConverter))]
    public abstract class QuestionAttempt
    {
        public int Id { get; set; }
        [Required]
        public int QuizQuestionId { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; }
        public double Grade { get; set; }
        public bool IsGraded { get; set; }
        public abstract double GradeQuestion();
    }
    public class MCQAttmept : QuestionAttempt
    {
        [MinimumListLength(minNumberofElements: 1)]
        public List<Choice> Choices { get; set; }
        public override double GradeQuestion()
        {
            IsGraded = true;
            MultipleChoiceQuestion mcq = QuizQuestion.Question as MultipleChoiceQuestion;
            double grade = mcq.Choices.Where(choice => choice.IsAnswer).Count(rightChoice => Choices.Any(answer => answer.Id == rightChoice.Id)) / mcq.Choices.Count;
            return Math.Round(grade, 2) * QuizQuestion.Grade;
        }
    }
    public class TrueFalseAttempt : QuestionAttempt
    {
        public bool Answer { get; set; }
        public override double GradeQuestion()
        {
            IsGraded = true;
            TrueFalseQuestion tfQuestion = QuizQuestion.Question as TrueFalseQuestion;
            return tfQuestion.Answer == Answer ? QuizQuestion.Grade : 0;
        }
    }
}
