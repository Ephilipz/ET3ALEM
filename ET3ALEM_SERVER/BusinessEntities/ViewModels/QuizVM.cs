using System;

namespace BusinessEntities.ViewModels
{
    public class QuizVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool NoDueDate { get; set; }
        public int DurationSeconds { get; set; }
        public bool UnlimitedTime { get; set; }
        public int AllowedAttempts { get; set; }
        public bool UnlimitedAttempts { get; set; }
        public bool ShowGrade { get; set; }
        public bool AutoGrade { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool IncludeAllQuestions { get; set; }
        public int? IncludedQuestionsCount { get; set; }
    }
}