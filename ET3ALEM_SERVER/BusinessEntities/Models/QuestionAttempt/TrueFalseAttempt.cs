namespace BusinessEntities.Models
{
    public class TrueFalseAttempt : QuestionAttempt
    {
        public bool Answer { get; set; }
        public bool IsAnswered { get; set; }

        public override void GradeQuestion()
        {
            IsGraded = true;
            var tfQuestion = QuizQuestion.Question as TrueFalseQuestion;
            double grade = (tfQuestion.Answer == Answer && IsAnswered) ? QuizQuestion.Grade : 0;
            Grade = grade;
        }
    }
}