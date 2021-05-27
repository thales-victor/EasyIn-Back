using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyIn.Migrations
{
    public partial class QRCODE_LOGIN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QrCodeLogin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Removed = table.Column<bool>(nullable: false),
                    CredentialId = table.Column<int>(nullable: true),
                    PlatformId = table.Column<int>(nullable: true),
                    BrowserToken = table.Column<string>(type: "varchar(100)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QrCodeLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QrCodeLogin_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QrCodeLogin_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QrCodeLogin_CredentialId",
                table: "QrCodeLogin",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_QrCodeLogin_PlatformId",
                table: "QrCodeLogin",
                column: "PlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QrCodeLogin");
        }
    }
}
