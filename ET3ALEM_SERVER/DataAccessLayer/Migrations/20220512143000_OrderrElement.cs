using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class OrderrElement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortAnswerAttempt_Answer",
                table: "QuestionAttempt",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "48a05d2c-985d-445d-b79c-94219ee3af9a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afa86a57-2626-4963-b6f4-708167ace85c", "AQAAAAEAACcQAAAAEB8jvcNaXVYkOVfYhFGHNuJHSWrtF+QWv6yFpwJuzXvSuSDtt2Y/6UfwkJYyFtzykg==", "760da905-e385-4ac5-b97c-f8056b7668fd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortAnswerAttempt_Answer",
                table: "QuestionAttempt");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "9dee8793-dce4-42c3-9baa-19290a04876b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb64cc9d-19ce-4eee-95ce-77141db40454", "AQAAAAEAACcQAAAAENKGOOHK4htpwlUF5g10ZzkztJaGXZCiRNXlMvCe3uYfk/rPfxHV2nFiVOeqMRMOUA==", "8dd6f4e4-faaf-4845-9026-0ebde84afb74" });
        }
    }
}
