using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleApi.Migrations
{
    public partial class editRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifyTime",
                table: "Roles",
                newName: "UpdateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2021, 4, 21, 21, 45, 48, 523, DateTimeKind.Local).AddTicks(7936));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Roles",
                newName: "LastModifyTime");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "LastModifyTime",
                value: new DateTime(2021, 4, 21, 21, 40, 22, 769, DateTimeKind.Local).AddTicks(8416));
        }
    }
}
