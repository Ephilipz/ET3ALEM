using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class OrderQuestion : Question
    {
        public virtual List<OrderedChoice> OrderedChoices { get; set; }
        public string CorrectOrderIds { get; set; }

    }
}
