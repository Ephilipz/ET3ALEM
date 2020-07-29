using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities.Enumerators;

namespace BusinessEntities.Models
{
    public class TrueFalseQuestion:Question
    {
        public bool Answer { get; set; }
        public TrueFalseQuestion()
        {
            this.QuestionType = QuestionType.TrueFalse;
        }
    }
}
