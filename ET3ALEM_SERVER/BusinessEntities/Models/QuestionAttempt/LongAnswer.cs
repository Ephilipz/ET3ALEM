namespace BusinessEntities.Models
{
    public class LongAnswer
    {
        public LongAnswer(int id, int longAnswerAttemptId, string answer)
        {
            Id = id;
            LongAnswerAttemptId = longAnswerAttemptId;
            Answer = answer;
        }

        public int Id { get; set; }
        public int LongAnswerAttemptId { get; set; }
        public string Answer { get; set; }
    }
}