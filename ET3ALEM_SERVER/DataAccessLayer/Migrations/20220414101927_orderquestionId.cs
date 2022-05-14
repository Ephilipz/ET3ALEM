using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class orderquestionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "6fe5ba5f-1861-43b5-84e2-8b45e60b11f2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be658556-9dd3-4d1b-98b9-9dd542878756", "AQAAAAEAACcQAAAAEPK5eqzbWLtYDsRnAsYPU4AdnwoB9Azn4R5fmfB2F3PZ3MS1aTKTy9FbjvsQ1bMhEg==", "84ad7d2c-652c-4b8f-b52c-429cd768b584" });
        }
    }
}
