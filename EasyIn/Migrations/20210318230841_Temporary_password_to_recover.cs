using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyIn.Migrations
{
    public partial class Temporary_password_to_recover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTemporaryPassword",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTemporaryPassword",
                table: "User");
        }
    }
}
