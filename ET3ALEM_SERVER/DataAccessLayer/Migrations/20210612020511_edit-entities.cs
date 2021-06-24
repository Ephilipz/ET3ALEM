using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class editentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choice_Question_MultipleChoiceQuestionId",
                table: "Choice");

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_Question_MCQId",
                table: "Choice",
                column: "MCQId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choice_Question_MCQId",
                table: "Choice");

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_Question_MultipleChoiceQuestionId",
                table: "Choice",
                column: "MCQId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
