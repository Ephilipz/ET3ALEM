using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessEntities.Models
{
    public class Choice
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        public int MCQId { get; set; }
        public virtual ICollection<MCQAttmept> MCQAttmepts { get; set; }

    }
}
