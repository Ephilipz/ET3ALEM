using System;
using System.Collections.Generic;


namespace BusinessEntities.ViewModels
{
    public class QuizAttemptVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartTime { get; set; }
        public QuizVM Quiz { get; set; }
        public List<QuestionAttemptVM> QuestionsAttempts { get; set; }
    }
}
