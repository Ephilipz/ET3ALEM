using System.ComponentModel.DataAnnotations;
using BusinessEntities.CustomConverters;
using Newtonsoft.Json;

namespace BusinessEntities.Models
{
    [JsonConverter(typeof(QuestionAttemptConverter))]
    public abstract class QuestionAttempt
    {
        public int Id { get; set; }

        [Required] public int QuizQuestionId { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; }
        public double Grade { get; protected set; }
        public bool IsGraded { get; set; }
        public int Sequence { get; set; }
        public abstract void GradeQuestion();
    }
}