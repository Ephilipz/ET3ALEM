using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class quizCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "Quiz");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Quiz",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Quiz");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "Quiz",
                type: "text",
                nullable: true);
        }
    }
}
