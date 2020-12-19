using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class addQuizUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Quiz",
                nullable: false,
                defaultValue: "",
                maxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_UserId",
                table: "Quiz",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_AspNetUsers_UserId",
                table: "Quiz",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_AspNetUsers_UserId",
                table: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_UserId",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quiz");
        }
    }
}
