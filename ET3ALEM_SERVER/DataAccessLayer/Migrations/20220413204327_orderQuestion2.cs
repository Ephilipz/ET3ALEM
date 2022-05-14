using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class orderQuestion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedElement_Question_OrderQuestionId1",
                table: "OrderedElement");

            migrationBuilder.DropIndex(
                name: "IX_OrderedElement_OrderQuestionId1",
                table: "OrderedElement");

            migrationBuilder.DropColumn(
                name: "OrderQuestionId1",
                table: "OrderedElement");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderQuestionId1",
                table: "OrderedElement",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301D884-221A-4E7D-B509-0113DCC043E1",
                column: "ConcurrencyStamp",
                value: "53165bb6-a5f6-4799-924e-527cafa1e8a1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B22698B8-42A2-4115-9631-1C2D1E2AC5F7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acbd91ff-d9b9-4ef8-9261-bd6b88008d79", "AQAAAAEAACcQAAAAEAs164NGG0l1/UBjEsmNcX3EoC68Ls+KtDPC5nWhY+S4r2z0dcL/uhs0YRUT5+AuEg==", "982f63aa-d1ee-436b-97aa-0e7f28d12d08" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedElement_OrderQuestionId1",
                table: "OrderedElement",
                column: "OrderQuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedElement_Question_OrderQuestionId1",
                table: "OrderedElement",
                column: "OrderQuestionId1",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
