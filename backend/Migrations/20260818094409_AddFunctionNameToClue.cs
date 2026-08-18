using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseFile.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFunctionNameToClue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FunctionName",
                table: "Clues",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionName",
                table: "Clues");
        }
    }
}
