using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace BusinessEntities.Models
{
    public class QuestionCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        [JsonIgnore]
        public List<Question> Questions
        {
            get
            {
                return TrueFalseQuestions.Concat<Question>(MultipleChoiceQuestions).ToList();
            }
        }
        public List<TrueFalseQuestion> TrueFalseQuestions { get; set; } = new List<TrueFalseQuestion>();
        public List<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; } = new List<MultipleChoiceQuestion>();
    }
}
