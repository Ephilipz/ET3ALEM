using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using BusinessEntities.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Server_Application.Data
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Question")
                .HasDiscriminator<QuestionType>("QuestionType")
                .HasValue<MultipleChoiceQuestion>(QuestionType.MCQ)
                .HasValue<TrueFalseQuestion>(QuestionType.TrueFalse)
                .HasValue<ShortAnswerQuestion>(QuestionType.ShortAnswer);
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
               .HasValue<ShortAnswerAttempt>(QuestionType.ShortAnswer);

            modelBuilder.Entity<MCQAttmept>()
               .HasMany(mcqAttmep => mcqAttmep.Choices)
               .WithMany(choice => choice.MCQAttmepts)
               .UsingEntity(j => j.ToTable("ChoiceMcqAttempt"));

            modelBuilder.Entity<ContactUsMessage>().ToTable("ContactUsMessage");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<QuestionCollection> QuestionCollections { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuestionAttempt> QuestionAttempts { get; set; }
        public DbSet<ContactUsMessage> ContactUsMessages { get; set; }
    }
}
