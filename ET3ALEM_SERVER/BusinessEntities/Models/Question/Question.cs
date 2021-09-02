#nullable enable
using System.ComponentModel.DataAnnotations;
using BusinessEntities.CustomConverters;
using BusinessEntities.Enumerators;
using Newtonsoft.Json;

namespace BusinessEntities.Models
{
    [JsonConverter(typeof(QuestionConverter))]
    public abstract class Question
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; protected set; }

        [Required] public string Body { get; set; }

        public int? QuestionCollectionId { get; set; }
        public string? Comment { get; set; }
    }
}