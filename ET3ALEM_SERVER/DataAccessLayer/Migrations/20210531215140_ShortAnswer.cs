using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ShortAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "QuestionAttempt",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "QuestionAttempt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TrueFalseAttempt_Answer",
                table: "QuestionAttempt",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CaseSensitive",
                table: "Question",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PossibleAnswers",
                table: "Question",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "QuestionAttempt");

            migrationBuilder.DropColumn(
                name: "TrueFalseAttempt_Answer",
                table: "QuestionAttempt");

            migrationBuilder.DropColumn(
                name: "CaseSensitive",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "PossibleAnswers",
                table: "Question");

            migrationBuilder.AlterColumn<bool>(
                name: "Answer",
                table: "QuestionAttempt",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
