using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;

namespace BusinessEntities.Models
{
    public class LongAnswerQuestion : Question
    {
        public LongAnswerQuestion()
        {
            QuestionType = QuestionType.LongAnswer;
        }
    }
}
