using System.Collections.Generic;

namespace BusinessEntities.Models
{
    public class OrderQuestion : Question
    {
        public virtual List<OrderedChoice> OrderedChoices { get; set; }
        public string CorrectOrderIds { get; set; }
    }
}