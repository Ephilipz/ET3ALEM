namespace BusinessEntities.Models
{
    public class LongAnswerAttempt : QuestionAttempt
    {
        public LongAnswer LongAnswer { get; set; }
        
        public override void GradeQuestion()
        {
            IsGraded = false;
        }
    }
}