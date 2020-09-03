using BusinessEntities.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public abstract class Question
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; protected set; }
        [Required]
        public string Body { get; set; }
        public int? QuestionCollectionId { get; set; }
    }
}
