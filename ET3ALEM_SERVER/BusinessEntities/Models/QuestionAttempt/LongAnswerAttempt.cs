using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class LongAnswerAttempt : QuestionAttempt
    {
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


        //Cannot auto grade long answer quesiton attempts so returns false for isGraded
        public override void GradeQuestion()
        {
            IsGraded = false;
        }
    }
}
