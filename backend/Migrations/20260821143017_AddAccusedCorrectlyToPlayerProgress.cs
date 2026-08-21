using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseFile.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAccusedCorrectlyToPlayerProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccusedCorrectly",
                table: "PlayerProgress",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccusedCorrectly",
                table: "PlayerProgress");
        }
    }
}
