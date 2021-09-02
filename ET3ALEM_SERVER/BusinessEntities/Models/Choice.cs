using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.Models
{
    public class Choice
    {
        public int Id { get; set; }

        [Required] public string Body { get; set; }

        public bool IsAnswer { get; set; }
        public int MCQId { get; set; }
        public virtual ICollection<MCQAttmept> MCQAttmepts { get; set; }
    }
}