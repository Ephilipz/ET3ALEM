using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities.Models
{
    public class Quiz
    {
        public Quiz()
        {
            QuizQuestions = new List<QuizQuestion>();
        }
        public int Id { get; set; }
        public string code { get; set; }
        [Required]
        public string Name { get; set; }
        public string instructions { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }
        public int TotalGrade { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool NoDueDate { get; set; }
        public int DurationSeconds { get; set; }
        public bool UnlimitedTime { get; set; }
    }
}
