using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities.Models
{
    public class LongAnswer
    {
        public LongAnswer(int id, int longAnswerAttemptId, string answer)
        {
            Id = id;
            Answer = answer;
        }

        public int Id { get; set; }

        public LongAnswerAttempt LongAnswerAttempt { get; set; }
        public int LongAnswerAttemptId { get; set; }
        public string Answer { get; set; }
    }
}