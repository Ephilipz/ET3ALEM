using Microsoft.AspNetCore.Identity;
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
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Instructions { get; set; }
        public virtual List<QuizQuestion> QuizQuestions { get; set; }
        public int TotalGrade { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool NoDueDate { get; set; }
        public int DurationSeconds { get; set; }
        public bool UnlimitedTime { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public int AllowedAttempts { get; set; }
        public bool UnlimitedAttempts { get; set; }
        public bool ShowGrade { get; set; }
        public bool ShuffleQuestions { get; set; }
        public string? NonShuffleQuestions { get; set; }
    }
}
