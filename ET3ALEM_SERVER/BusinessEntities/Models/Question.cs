using BusinessEntities.Enumerators;
using BusinessEntities.CustomConverters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.Models
{
    [JsonConverter(typeof(QuestionConverter))]
    public abstract class Question
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; protected set; }
        [Required]
        public string Body { get; set; }
        public int? QuestionCollectionId { get; set; }
    }
}
