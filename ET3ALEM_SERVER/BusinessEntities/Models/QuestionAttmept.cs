using BusinessEntities.CustomConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
}
