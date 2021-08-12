using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Removeshowcorrectanswersinquiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowCorrectAnswers",
                table: "Quiz");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowCorrectAnswers",
                table: "Quiz",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
