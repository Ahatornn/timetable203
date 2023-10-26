using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTable203.Context.Migrations
{
    public partial class Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Group_id",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Group_id",
                table: "Persons");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
