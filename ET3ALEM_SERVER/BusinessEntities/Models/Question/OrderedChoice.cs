using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Models
{
    public class OrderedChoice
    {
        public int Id { get; private set; }
        public OrderQuestion OrderQuestion { get; set; }
        public int OrderQuestionId { get; set; }
        public string Text { get; set; }
    }
}
