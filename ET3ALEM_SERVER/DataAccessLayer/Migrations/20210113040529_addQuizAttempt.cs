using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Migrations
{
    public partial class addQuizAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MCQAttmeptId",
                table: "Choice",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuizAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    QuizId = table.Column<int>(nullable: false),
                    Grade = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    QuizQuestionId = table.Column<int>(nullable: false),
                    Grade = table.Column<double>(nullable: false),
                    IsGraded = table.Column<bool>(nullable: false),
                    QuestionType = table.Column<int>(nullable: false),
                    QuizAttemptId = table.Column<int>(nullable: true),
                    Answer = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAttempt_QuizAttempt_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionAttempt_QuizQuestion_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choice_MCQAttmeptId",
                table: "Choice",
                column: "MCQAttmeptId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempt_QuizAttemptId",
                table: "QuestionAttempt",
                column: "QuizAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempt_QuizQuestionId",
                table: "QuestionAttempt",
                column: "QuizQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_QuestionAttempt_MCQAttmeptId",
                table: "Choice",
                column: "MCQAttmeptId",
                principalTable: "QuestionAttempt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choice_QuestionAttempt_MCQAttmeptId",
                table: "Choice");

            migrationBuilder.DropTable(
                name: "QuestionAttempt");

            migrationBuilder.DropTable(
                name: "QuizAttempt");

            migrationBuilder.DropIndex(
                name: "IX_Choice_MCQAttmeptId",
                table: "Choice");

            migrationBuilder.DropColumn(
                name: "MCQAttmeptId",
                table: "Choice");
        }
    }
}
