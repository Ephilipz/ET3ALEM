using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class QuizOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitTime",
                table: "QuizAttempt",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AllowedAttempts",
                table: "Quiz",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NonShuffleQuestions",
                table: "Quiz",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowGrade",
                table: "Quiz",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShuffleQuestions",
                table: "Quiz",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UnlimitedAttempts",
                table: "Quiz",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitTime",
                table: "QuizAttempt");

            migrationBuilder.DropColumn(
                name: "AllowedAttempts",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "NonShuffleQuestions",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "ShowGrade",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "ShuffleQuestions",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "UnlimitedAttempts",
                table: "Quiz");
        }
    }
}
