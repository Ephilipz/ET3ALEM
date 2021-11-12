namespace BusinessEntities.ViewModels
{
    public class QuizQuestionVM
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public int Grade { get; set; }
        public int QuestionId { get; set; }
        public QuestionVM Question { get; set; }
    }
}