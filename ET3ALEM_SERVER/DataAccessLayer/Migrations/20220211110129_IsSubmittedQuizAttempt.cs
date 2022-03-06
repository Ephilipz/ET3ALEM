using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class IsSubmittedQuizAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "QuizAttempt",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "6e2c7b15-36e6-4321-978b-e14e9178074a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1729689-670a-4010-91ab-3ba298755f2d", "AQAAAAEAACcQAAAAEM2DmoQ1bCAk0gE7S2urROADwkTyzJyzOn8CKk3ieDN7SU34xJp5AU6pbw3GLiecCA==", "c5299ecc-8c59-470d-b3ea-02c1c909ce1d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "QuizAttempt");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "b0842d52-45b2-4a84-bf60-c9c33ddc694f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "169b1f71-4c87-43ac-b123-49b1d958ad08", "AQAAAAEAACcQAAAAEOBBoLesW/FBBTySYa0FhCh1apPSdAgpVPsgIiIszLxRROXA0GcbRvOOFuEg9UEG0Q==", "a23f830a-de8b-4528-aac1-8bcc9bfd77ac" });
        }
    }
}
