using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseFile.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToPlayerProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerProgress_CaseId",
                table: "PlayerProgress");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_CaseId_PlayerSessionId",
                table: "PlayerProgress",
                columns: new[] { "CaseId", "PlayerSessionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerProgress_CaseId_PlayerSessionId",
                table: "PlayerProgress");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_CaseId",
                table: "PlayerProgress",
                column: "CaseId");
        }
    }
}
