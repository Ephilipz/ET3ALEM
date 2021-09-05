namespace BusinessEntities.Models
{
    public class LongAnswerAttempt : QuestionAttempt
    {
#nullable enable
        public LongAnswer? LongAnswer { get; set; }

        public LongAnswerAttempt()
        {
        }

        public LongAnswerAttempt(LongAnswer longAnswer)
        {
            LongAnswer = longAnswer;
        }

        public LongAnswerAttempt(string answer)
        {
            LongAnswer = new LongAnswer(0, Id, answer);
        }


        //Cannot auto grade long answer question attempts so returns false for isGraded
        public override void GradeQuestion()
        {
            IsGraded = false;
        }
    }
}