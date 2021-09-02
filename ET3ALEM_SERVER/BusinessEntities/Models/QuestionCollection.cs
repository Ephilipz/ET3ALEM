using System;
using System.Collections.Generic;

namespace BusinessEntities.Models
{
    public class QuestionCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}