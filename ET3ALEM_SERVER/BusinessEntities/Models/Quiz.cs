using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.Models
{
    public class Quiz : IEquatable<Quiz>
    {
        public Quiz()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }

        [Required] public string Name { get; set; }

        public string Instructions { get; set; }
        public virtual List<QuizQuestion> QuizQuestions { get; set; } = new();
        public virtual List<QuizAttempt> QuizAttempts { get; set; }
        public int TotalGrade { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool NoDueDate { get; set; }
        public int DurationSeconds { get; set; }
        public bool UnlimitedTime { get; set; }
        [Required] public string UserId { get; set; }
        public virtual User User { get; set; }
        public int AllowedAttempts { get; set; }
        public bool UnlimitedAttempts { get; set; }
        public bool ShowGrade { get; set; }
        public bool AutoGrade { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool IncludeAllQuestions { get; set; }
        public int? IncludedQuestionsCount { get; set; }

        public bool Equals(Quiz other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Code, other.Code, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Quiz) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Code, StringComparer.InvariantCultureIgnoreCase);
            return hashCode.ToHashCode();
        }

        public static bool operator ==(Quiz left, Quiz right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Quiz left, Quiz right)
        {
            return !Equals(left, right);
        }
    }
}