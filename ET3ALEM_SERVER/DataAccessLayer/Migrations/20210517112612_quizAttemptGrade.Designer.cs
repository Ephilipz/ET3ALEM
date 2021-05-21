﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server_Application.Data;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210517112612_quizAttemptGrade")]
    partial class quizAttemptGrade
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BusinessEntities.Models.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAnswer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("MCQId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MCQId");

                    b.ToTable("Choice");
                });

            modelBuilder.Entity("BusinessEntities.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("QuestionCollectionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionCollectionId");

                    b.ToTable("Question");

                    b.HasDiscriminator<int>("QuestionType");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuestionAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Grade")
                        .HasColumnType("double");

                    b.Property<bool>("IsGraded")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.Property<int?>("QuizAttemptId")
                        .HasColumnType("int");

                    b.Property<int>("QuizQuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuizAttemptId");

                    b.HasIndex("QuizQuestionId");

                    b.ToTable("QuestionAttempt");

                    b.HasDiscriminator<int>("QuestionType");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuestionCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionCollection");
                });

            modelBuilder.Entity("BusinessEntities.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AllowedAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("AutoGrade")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DurationSeconds")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Instructions")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("NoDueDate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NonShuffleQuestions")
                        .HasColumnType("longtext");

                    b.Property<bool>("ShowCorrectAnswers")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ShowGrade")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ShuffleQuestions")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TotalGrade")
                        .HasColumnType("int");

                    b.Property<bool>("UnlimitedAttempts")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("UnlimitedTime")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuizAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Grade")
                        .HasColumnType("double");

                    b.Property<bool>("IsGraded")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("SubmitTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("QuizAttempt");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuizQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizQuestion");
                });

            modelBuilder.Entity("BusinessEntities.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ChoiceMCQAttmept", b =>
                {
                    b.Property<int>("ChoicesId")
                        .HasColumnType("int");

                    b.Property<int>("MCQAttmeptsId")
                        .HasColumnType("int");

                    b.HasKey("ChoicesId", "MCQAttmeptsId");

                    b.HasIndex("MCQAttmeptsId");

                    b.ToTable("ChoiceMcqAttempt");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BusinessEntities.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("BusinessEntities.Models.Question");

                    b.Property<int>("McqAnswerType")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("BusinessEntities.Models.TrueFalseQuestion", b =>
                {
                    b.HasBaseType("BusinessEntities.Models.Question");

                    b.Property<bool>("Answer")
                        .HasColumnType("tinyint(1)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("BusinessEntities.Models.MCQAttmept", b =>
                {
                    b.HasBaseType("BusinessEntities.Models.QuestionAttempt");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("BusinessEntities.Models.TrueFalseAttempt", b =>
                {
                    b.HasBaseType("BusinessEntities.Models.QuestionAttempt");

                    b.Property<bool>("Answer")
                        .HasColumnType("tinyint(1)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("BusinessEntities.Models.Choice", b =>
                {
                    b.HasOne("BusinessEntities.Models.MultipleChoiceQuestion", "MCQ")
                        .WithMany("Choices")
                        .HasForeignKey("MCQId");

                    b.Navigation("MCQ");
                });

            modelBuilder.Entity("BusinessEntities.Models.Question", b =>
                {
                    b.HasOne("BusinessEntities.Models.QuestionCollection", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuestionCollectionId");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuestionAttempt", b =>
                {
                    b.HasOne("BusinessEntities.Models.QuizAttempt", null)
                        .WithMany("QuestionsAttempts")
                        .HasForeignKey("QuizAttemptId");

                    b.HasOne("BusinessEntities.Models.QuizQuestion", "QuizQuestion")
                        .WithMany()
                        .HasForeignKey("QuizQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuizQuestion");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuestionCollection", b =>
                {
                    b.HasOne("BusinessEntities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessEntities.Models.Quiz", b =>
                {
                    b.HasOne("BusinessEntities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuizAttempt", b =>
                {
                    b.HasOne("BusinessEntities.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessEntities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuizQuestion", b =>
                {
                    b.HasOne("BusinessEntities.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessEntities.Models.Quiz", null)
                        .WithMany("QuizQuestions")
                        .HasForeignKey("QuizId");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ChoiceMCQAttmept", b =>
                {
                    b.HasOne("BusinessEntities.Models.Choice", null)
                        .WithMany()
                        .HasForeignKey("ChoicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessEntities.Models.MCQAttmept", null)
                        .WithMany()
                        .HasForeignKey("MCQAttmeptsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BusinessEntities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BusinessEntities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessEntities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BusinessEntities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessEntities.Models.QuestionCollection", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("BusinessEntities.Models.Quiz", b =>
                {
                    b.Navigation("QuizQuestions");
                });

            modelBuilder.Entity("BusinessEntities.Models.QuizAttempt", b =>
                {
                    b.Navigation("QuestionsAttempts");
                });

            modelBuilder.Entity("BusinessEntities.Models.MultipleChoiceQuestion", b =>
                {
                    b.Navigation("Choices");
                });
#pragma warning restore 612, 618
        }
    }
}
