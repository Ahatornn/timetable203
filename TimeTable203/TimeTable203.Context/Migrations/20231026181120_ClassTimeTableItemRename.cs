using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTable203.Context.Migrations
{
    public partial class ClassTimeTableItemRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "TimeTableItems",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TimeTableItems");
        }
    }
}
