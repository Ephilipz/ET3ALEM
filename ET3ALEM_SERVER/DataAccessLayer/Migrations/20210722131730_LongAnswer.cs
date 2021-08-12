using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class LongAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LongAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LongAnswerAttemptId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LongAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LongAnswer_QuestionAttempt_LongAnswerAttemptId",
                        column: x => x.LongAnswerAttemptId,
                        principalTable: "QuestionAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LongAnswer_LongAnswerAttemptId",
                table: "LongAnswer",
                column: "LongAnswerAttemptId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LongAnswer");
        }
    }
}
