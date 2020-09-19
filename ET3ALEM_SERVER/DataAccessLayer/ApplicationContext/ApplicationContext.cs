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
            modelBuilder.Entity<QuizQuestion>().ToTable("QuizQuestion");
            modelBuilder.Entity<Quiz>().ToTable("Quiz");
            modelBuilder.Entity<Choice>().ToTable("Choice");
            modelBuilder.Entity<Choice>()
                .HasOne(choice => choice.MCQ)
                .WithMany(mcq => mcq.Choices);
            modelBuilder.Entity<QuestionCollection>().ToTable("QuestionCollection");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<QuestionCollection> QuestionCollections { get; set; }
    }
}
