using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class add_question_collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionCollectionId",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionCollection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionCollectionId",
                table: "Question",
                column: "QuestionCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCollection_UserId",
                table: "QuestionCollection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__QuestionCollectionId",
                table: "Question",
                column: "QuestionCollectionId",
                principalTable: "QuestionCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionCollection_QuestionCollectionId",
                table: "Question");

            migrationBuilder.DropTable(
                name: "QuestionCollection");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuestionCollectionId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionCollectionId",
                table: "Question");
        }
    }
}
