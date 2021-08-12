using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class LongAnswer
    {
        public int Id { get; set; }
        public int LongAnswerAttemptId { get; set; }
        public string Answer { get; set; }
        public LongAnswer(int id, int longAnswerAttemptId, string answer)
        {
            Id = id;
            LongAnswerAttemptId = longAnswerAttemptId;
            Answer = answer;
        }
    }
}
