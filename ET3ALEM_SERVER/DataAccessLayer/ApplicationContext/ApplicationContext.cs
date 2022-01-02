using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server_Application.Data
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<QuestionCollection> QuestionCollections { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuestionAttempt> QuestionAttempts { get; set; }
        public DbSet<ContactUsMessage> ContactUsMessages { get; set; }
        public DbSet<LongAnswer> LongAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Question")
                .HasDiscriminator<QuestionType>("QuestionType")
                .HasValue<MultipleChoiceQuestion>(QuestionType.MCQ)
                .HasValue<TrueFalseQuestion>(QuestionType.TrueFalse)
                .HasValue<ShortAnswerQuestion>(QuestionType.ShortAnswer)
                .HasValue<LongAnswerQuestion>(QuestionType.LongAnswer);
            modelBuilder.Entity<MultipleChoiceQuestion>().HasMany(mcq => mcq.Choices).WithOne().HasForeignKey("MCQId");
            modelBuilder.Entity<QuizQuestion>().ToTable("QuizQuestion");

            modelBuilder.Entity<Quiz>().ToTable("Quiz");

            modelBuilder.Entity<Choice>().ToTable("Choice");

            modelBuilder.Entity<QuestionCollection>().ToTable("QuestionCollection");

            modelBuilder.Entity<QuizAttempt>().ToTable("QuizAttempt");

            modelBuilder.Entity<QuestionAttempt>().ToTable("QuestionAttempt")
                .HasDiscriminator<QuestionType>("QuestionType")
                .HasValue<MCQAttmept>(QuestionType.MCQ)
                .HasValue<TrueFalseAttempt>(QuestionType.TrueFalse)
                .HasValue<ShortAnswerAttempt>(QuestionType.ShortAnswer)
                .HasValue<LongAnswerAttempt>(QuestionType.LongAnswer);

            modelBuilder.Entity<MCQAttmept>()
                .HasMany(mcqAttempt => mcqAttempt.Choices)
                .WithMany(choice => choice.MCQAttmepts)
                .UsingEntity(j => j.ToTable("ChoiceMcqAttempt"));

            modelBuilder.Entity<ContactUsMessage>().ToTable("ContactUsMessage");

            modelBuilder.Entity<LongAnswer>().ToTable("LongAnswer");
            modelBuilder.Entity<LongAnswer>()
                .HasOne(longAnswer => longAnswer.LongAnswerAttempt)
                .WithOne(attempt => attempt.LongAnswer)
                .HasForeignKey<LongAnswer>(longAnswer =>
                    longAnswer.LongAnswerAttemptId);

            base.OnModelCreating(modelBuilder);
        }
    }
}