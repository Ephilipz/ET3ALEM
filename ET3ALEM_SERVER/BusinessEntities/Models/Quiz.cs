﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Validation.CustomValidationAttributes;

namespace BusinessEntities.Models
{
    public class Quiz
    {
        public Quiz()
        {
            QuizQuestions = new List<QuizQuestion>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MinimumListLength(minNumberofElements: 1)]
        public List<QuizQuestion> QuizQuestions { get; set; }
        public int TotalGrade { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}