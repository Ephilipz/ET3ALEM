using BusinessEntities.Enumerators;

namespace BusinessEntities.Models
{
    public class ShortAnswerQuestion : Question
    {
        public ShortAnswerQuestion()
        {
            QuestionType = QuestionType.ShortAnswer;
        }

        public string PossibleAnswers { get; set; }
        public bool CaseSensitive { get; set; }
    }
}