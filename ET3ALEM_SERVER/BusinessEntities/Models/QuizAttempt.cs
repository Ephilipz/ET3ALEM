using BusinessEntities.CustomConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Validation.CustomValidationAttributes;

namespace BusinessEntities.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int QuizId { get; set; }
        [MinimumListLength(minNumberofElements: 1)]
        public List<QuestionAttempt> QuestionsAttempts { get; set; }
        public double Grade { get; set; }
        public bool IsGraded => QuestionsAttempts.Count(questionAttempt => questionAttempt.IsGraded) == QuestionsAttempts.Count;
    }
}
