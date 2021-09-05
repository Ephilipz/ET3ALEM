namespace BusinessEntities.ViewModels
{
    public class QuestionAttemptVM
    {
        public int Id { get; set; }
        public string LongAnswer { get; set; }
        public QuizQuestionVM QuizQuestion { get; set; }
    }
}