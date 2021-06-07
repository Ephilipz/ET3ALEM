using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class QuestionCount_QuestionComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonShuffleQuestions",
                table: "Quiz");

            migrationBuilder.AddColumn<bool>(
                name: "IncludeAllQuestions",
                table: "Quiz",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IncludedQuestionsCount",
                table: "Quiz",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Question",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeAllQuestions",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "IncludedQuestionsCount",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Question");

            migrationBuilder.AddColumn<string>(
                name: "NonShuffleQuestions",
                table: "Quiz",
                type: "longtext",
                nullable: true);
        }
    }
}
