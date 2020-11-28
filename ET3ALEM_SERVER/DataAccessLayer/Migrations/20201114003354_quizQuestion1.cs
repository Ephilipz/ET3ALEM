using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class quizQuestion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "QuizQuestion");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuizQuestion_QuestionId",
                table: "QuizQuestion");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "QuizQuestion",
                type: "time",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuestionId",
                table: "QuizQuestion",
                column: "QuestionId");
        }
    }
}
