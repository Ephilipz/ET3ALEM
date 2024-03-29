﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }

        [Required] public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required] public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public DateTime StartTime { get; set; }
        public List<QuestionAttempt> QuestionsAttempts { get; set; }
        public double Grade { get; set; }
        public bool IsGraded { get; set; }
        public bool IsSubmitted { get; set; }
        public DateTime? SubmitTime { get; set; }

        public void GradeQuiz()
        {
            Grade = 0;
            IsGraded = true;
            QuestionsAttempts.ForEach(qA =>
            {
                qA.GradeQuestion();
                Grade += qA.Grade;
                IsGraded &= qA.IsGraded;
            });
        }
    }
}