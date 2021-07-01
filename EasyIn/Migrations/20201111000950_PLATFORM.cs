using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyIn.Migrations
{
    public partial class PLATFORM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Removed = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'EasyIn')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Twitter')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Facebook')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Instagram')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Google')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Pinterest')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Linkedin')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Netflix')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Spotify')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Amazon')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Disney+')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Tumblr')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Mercado Livre')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Magazine Luiza')");
            migrationBuilder.Sql("INSERT INTO Platform (CreatedAt, Removed, Name) Values('2021-01-01 00:00:00', 0, 'Americanas')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
