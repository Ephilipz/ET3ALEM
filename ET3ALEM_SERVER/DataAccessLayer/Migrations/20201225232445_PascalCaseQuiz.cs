using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class PascalCaseQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "instructions",
                table: "Quiz",
                newName: "Instructions");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Quiz",
                newName: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Quiz",
                newName: "instructions");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Quiz",
                newName: "code");
        }
    }
}
