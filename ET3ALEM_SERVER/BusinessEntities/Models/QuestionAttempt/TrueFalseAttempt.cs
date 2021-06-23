using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class TrueFalseAttempt : QuestionAttempt
    {
        public bool Answer { get; set; }
        public override void GradeQuestion()
        {
            IsGraded = true;
            TrueFalseQuestion tfQuestion = QuizQuestion.Question as TrueFalseQuestion;
            double grade = tfQuestion.Answer == Answer ? QuizQuestion.Grade : 0;
            this.Grade = grade;
        }
    }
}
