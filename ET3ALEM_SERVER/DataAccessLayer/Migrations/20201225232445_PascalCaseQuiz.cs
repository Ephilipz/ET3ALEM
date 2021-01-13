using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class PascalCaseQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Quiz RENAME COLUMN instructions TO Instructions");
            migrationBuilder.Sql("ALTER TABLE Quiz RENAME COLUMN code TO Code");
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
