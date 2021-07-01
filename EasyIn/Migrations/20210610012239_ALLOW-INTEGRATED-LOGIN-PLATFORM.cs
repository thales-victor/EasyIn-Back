using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyIn.Migrations
{
    public partial class ALLOWINTEGRATEDLOGINPLATFORM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowIntegratedLogin",
                table: "Platform",
                nullable: false,
                defaultValue: false);


            migrationBuilder.Sql("UPDATE Platform SET AllowIntegratedLogin = 1 WHERE Name = 'EasyIn'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowIntegratedLogin",
                table: "Platform");
        }
    }
}
