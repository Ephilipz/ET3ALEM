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