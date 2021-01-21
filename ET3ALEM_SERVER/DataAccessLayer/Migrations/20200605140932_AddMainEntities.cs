using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddMainEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.AddColumn<int>(
                name: "McqAnswerType",
                table: "Question",
                nullable: false,
                defaultValue:0);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Question",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "QuestionType",
                table: "Question",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "Answer",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    IsAnswer = table.Column<bool>(nullable: false),
                    MCQId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choice_Question_MCQId",
                        column: x => x.MCQId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TotalGrade = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: true),
                    Grade = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: true),
                    QuizId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choice_MCQId",
                table: "Choice",
                column: "MCQId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuestionId",
                table: "QuizQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestion",
                column: "QuizId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropColumn(
                name: "McqAnswerType",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Question");

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });
        }
    }
}
