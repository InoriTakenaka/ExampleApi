using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleApi.Migrations
{
    public partial class addtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", nullable: true),
                    Auth = table.Column<string>(type: "TEXT", nullable: true),
                    Authorizer = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Auth", "Authorizer", "LastModifyTime", "RoleName" },
                values: new object[] { 1, "/index/home |\r\n                             /index/users|\r\n                             /index/roles", null, new DateTime(2021, 4, 21, 21, 40, 22, 769, DateTimeKind.Local).AddTicks(8416), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
