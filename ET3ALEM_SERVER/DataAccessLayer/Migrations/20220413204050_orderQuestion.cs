using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class orderQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectOrderIds",
                table: "Question",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderedElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderQuestionId1 = table.Column<int>(type: "int", nullable: true),
                    OrderQuestionId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedElement_Question_OrderQuestionId",
                        column: x => x.OrderQuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedElement_Question_OrderQuestionId1",
                        column: x => x.OrderQuestionId1,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_OrderedElement_OrderQuestionId",
                table: "OrderedElement",
                column: "OrderQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedElement_OrderQuestionId1",
                table: "OrderedElement",
                column: "OrderQuestionId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedElement");

            migrationBuilder.DropColumn(
                name: "CorrectOrderIds",
                table: "Question");

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
    }
}
