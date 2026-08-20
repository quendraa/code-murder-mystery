using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseFile.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetectiveName",
                table: "PlayerProgress");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "PlayerProgress",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DetectiveName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_PlayerId",
                table: "PlayerProgress",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_SessionId",
                table: "Player",
                column: "SessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerProgress_Player_PlayerId",
                table: "PlayerProgress",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerProgress_Player_PlayerId",
                table: "PlayerProgress");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropIndex(
                name: "IX_PlayerProgress_PlayerId",
                table: "PlayerProgress");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerProgress");

            migrationBuilder.AddColumn<string>(
                name: "DetectiveName",
                table: "PlayerProgress",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
