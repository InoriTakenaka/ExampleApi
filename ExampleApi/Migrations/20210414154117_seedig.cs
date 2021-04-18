using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleApi.Migrations
{
    public partial class seedig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "ParentId", "Text", "Url" },
                values: new object[] { 1, 0, "Home", "/index/home" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "ParentId", "Text", "Url" },
                values: new object[] { 2, 0, "Auth", "Auth" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "ParentId", "Text", "Url" },
                values: new object[] { 3, 2, "User Manage", "/index/users" });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "ParentId", "Text", "Url" },
                values: new object[] { 4, 2, "Roles Manage", "/index/roles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
