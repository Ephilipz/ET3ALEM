using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class longAnswerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "3a5de38d-d11d-4534-b7e6-954f94999889");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dfca8fd-8378-46d6-957f-a0d0ecbe1267", "AQAAAAEAACcQAAAAEBiOL3bMTDAmtto3SR6Si3AqBLvzWK4580T2pLrMQJGqdGSHLXQxt4nP/O5eG20olw==", "83c8cac3-e8ff-49cf-b716-7b53d5b6d6c6" });
        }
    }
}
