using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseFile.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAccusedSuspectIdToPlayerProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccusedSuspectId",
                table: "PlayerProgress",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccusedSuspectId",
                table: "PlayerProgress");
        }
    }
}
