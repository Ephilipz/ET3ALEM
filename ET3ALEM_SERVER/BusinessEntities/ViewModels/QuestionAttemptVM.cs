using BusinessEntities.Models;

namespace BusinessEntities.ViewModels
{
    public class QuestionAttemptVM
    {
        public int Id { get; set; }
        public QuizQuestionVM QuizQuestion { get; set; }
    }
    public class LongQuestionAttemptVM : QuestionAttemptVM
    { 
        public LongAnswer LongAnswer { get; set; }
    }
}