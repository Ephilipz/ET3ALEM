using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using BusinessEntities.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Application.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Question")
                .HasDiscriminator<QuestionType>("QuestionType")
                .HasValue<MultipleChoiceQuestion>(QuestionType.MCQ)
                .HasValue<TrueFalseQuestion>(QuestionType.TrueFalse);
            modelBuilder.Entity<QuizQuestion>().ToTable("QuizQuestion")
                .HasOne(q => q.Question)
                .WithOne()
                .HasConstraintName("FK_QuizQuestion_Question_QuestionId").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Quiz>().ToTable("Quiz")
                .HasMany(q => q.QuizQuestions)
                .WithOne().HasConstraintName("FK_QuizQuestion_Quiz_QuizId").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Choice>().ToTable("Choice");
            modelBuilder.Entity<Choice>()
                .HasOne(choice => choice.MCQ)
                .WithMany(mcq => mcq.Choices);
            modelBuilder.Entity<QuestionCollection>().ToTable("QuestionCollection");
            modelBuilder.Entity<QuizAttempt>().ToTable("QuizAttempt");
            modelBuilder.Entity<QuestionAttempt>().ToTable("QuestionAttempt")
               .HasDiscriminator<QuestionType>("QuestionType")
               .HasValue<MCQAttmept>(QuestionType.MCQ)
               .HasValue<TrueFalseAttempt>(QuestionType.TrueFalse);
            modelBuilder.Entity<MCQAttmept>().HasMany(mcqAttempt => mcqAttempt.Choices);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<QuestionCollection> QuestionCollections { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuestionAttempt> QuestionAttempts { get; set; }
    }
}
