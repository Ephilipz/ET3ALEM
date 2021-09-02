using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BusinessEntities.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public virtual ICollection<QuestionCollection> QuestionCollections { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}