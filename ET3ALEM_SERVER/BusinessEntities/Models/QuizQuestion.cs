using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessEntities.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int QuizId { get; set; }
        public Question Question { get; set; }
        public int Grade { get; set; }
    }
}
