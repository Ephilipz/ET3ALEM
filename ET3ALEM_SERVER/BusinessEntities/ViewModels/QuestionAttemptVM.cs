using System.Collections.Generic;
using BusinessEntities.Models;

namespace BusinessEntities.ViewModels
{
    public class QuestionAttemptVM
    {
        public int Id { get; set; }
        public int QuizQuestionId { get; set; }
        public QuizQuestionVM QuizQuestion { get; set; }
    }

    public class LongAnswerAttemptVM : QuestionAttemptVM
    {
        public LongAnswer LongAnswer { get; set; }
    }

    public class MCQAttemptVM : QuestionAttemptVM
    {
        public List<ChoiceVM> Choices { get; set; }
    }

    public class ShortAnswerAttemptVM : QuestionAttemptVM
    {
        public string Answer { get; set; }
    }

    public class TrueFalseAttemptVM : QuestionAttemptVM
    {
        public bool Answer { get; set; }
        public bool IsAnswered { get; set; }
    }
    
    public class OrderAttemptVM : QuestionAttemptVM
    {
        public bool Answer { get; set; }
    }
}