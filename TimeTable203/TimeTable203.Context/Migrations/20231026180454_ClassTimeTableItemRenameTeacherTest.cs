using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTable203.Context.Migrations
{
    public partial class ClassTimeTableItemRenameTeacherTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher_ID",
                table: "TimeTableItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Teacher_ID",
                table: "TimeTableItems",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
