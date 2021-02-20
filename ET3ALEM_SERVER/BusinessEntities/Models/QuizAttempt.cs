using BusinessEntities.CustomConverters;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BusinessEntities.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public DateTime StartTime { get; set; }
        public List<QuestionAttempt> QuestionsAttempts { get; set; }
        public double Grade { get; set; }
        public bool IsGraded => QuestionsAttempts != null ? QuestionsAttempts.Count(questionAttempt => questionAttempt.IsGraded) == QuestionsAttempts.Count : false;
        public DateTime? SubmitTime { get; set; }

        public void GradeQuiz()
        {
            QuestionsAttempts.ForEach(qA => qA.GradeQuestion());
        }
    }
}
