using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;

namespace BusinessEntities.Models
{
    public class ShortAnswerQuestion : Question
    {
        public string PossibleAnswers { get; set; }
        public bool CaseSensitive { get; set; }
        public ShortAnswerQuestion()
        {
            QuestionType = QuestionType.ShortAnswer;
        }
    }
}
