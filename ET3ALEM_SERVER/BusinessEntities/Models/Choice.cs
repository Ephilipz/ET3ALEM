using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessEntities.Models
{
    public class Choice
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        public virtual MultipleChoiceQuestion MCQ { get; set; }
        public virtual ICollection<MCQAttmept> MCQAttmepts { get; set; }

    }
}
