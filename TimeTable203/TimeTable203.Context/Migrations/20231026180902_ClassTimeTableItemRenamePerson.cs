using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTable203.Context.Migrations
{
    public partial class ClassTimeTableItemRenamePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTableItems_Persons_TeacherId",
                table: "TimeTableItems");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TimeTableItems",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTableItems_TeacherId",
                table: "TimeTableItems",
                newName: "IX_TimeTableItems_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTableItems_Persons_PersonId",
                table: "TimeTableItems",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTableItems_Persons_PersonId",
                table: "TimeTableItems");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "TimeTableItems",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTableItems_PersonId",
                table: "TimeTableItems",
                newName: "IX_TimeTableItems_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTableItems_Persons_TeacherId",
                table: "TimeTableItems",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
