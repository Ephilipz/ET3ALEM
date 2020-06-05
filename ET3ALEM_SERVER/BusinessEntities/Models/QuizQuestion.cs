using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessEntities.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        [Required]
        public Question Question { get; set; }
        public int Grade { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
