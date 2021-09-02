using BusinessEntities.Enumerators;

namespace BusinessEntities.Models
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion()
        {
            QuestionType = QuestionType.TrueFalse;
        }

        public bool Answer { get; set; }
    }
}