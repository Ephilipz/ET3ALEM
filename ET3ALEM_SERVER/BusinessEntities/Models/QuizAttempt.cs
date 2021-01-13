using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<QuestionAttempt> Attmepts { get; set; }
        public double Grade { get; set; }
    }
}
