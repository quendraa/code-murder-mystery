using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseFile.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IntroText = table.Column<string>(type: "text", nullable: false),
                    VictimName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SolutionText = table.Column<string>(type: "text", nullable: false),
                    KillerSuspectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerSessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentClueIndex = table.Column<int>(type: "integer", nullable: false),
                    SolvedClueIds = table.Column<string>(type: "text", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerProgress_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suspects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    AlibiText = table.Column<string>(type: "text", nullable: false),
                    IsKiller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suspects_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    SourceLabel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PuzzleType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PromptText = table.Column<string>(type: "text", nullable: false),
                    StarterCode = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    EvidenceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clues_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evidence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DescriptionText = table.Column<string>(type: "text", nullable: false),
                    LinkedSuspectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReinterpretedDescription = table.Column<string>(type: "text", nullable: true),
                    ReinterpretedAfterClueId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidence_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evidence_Clues_ReinterpretedAfterClueId",
                        column: x => x.ReinterpretedAfterClueId,
                        principalTable: "Clues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evidence_Suspects_LinkedSuspectId",
                        column: x => x.LinkedSuspectId,
                        principalTable: "Suspects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PuzzleTestCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Input = table.Column<string>(type: "text", nullable: false),
                    ExpectedOutput = table.Column<string>(type: "text", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuzzleTestCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PuzzleTestCases_Clues_ClueId",
                        column: x => x.ClueId,
                        principalTable: "Clues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_KillerSuspectId",
                table: "Cases",
                column: "KillerSuspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Clues_CaseId",
                table: "Clues",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Clues_EvidenceId",
                table: "Clues",
                column: "EvidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_CaseId",
                table: "Evidence",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_LinkedSuspectId",
                table: "Evidence",
                column: "LinkedSuspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidence_ReinterpretedAfterClueId",
                table: "Evidence",
                column: "ReinterpretedAfterClueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProgress_CaseId",
                table: "PlayerProgress",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PuzzleTestCases_ClueId",
                table: "PuzzleTestCases",
                column: "ClueId");

            migrationBuilder.CreateIndex(
                name: "IX_Suspects_CaseId",
                table: "Suspects",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Suspects_KillerSuspectId",
                table: "Cases",
                column: "KillerSuspectId",
                principalTable: "Suspects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clues_Evidence_EvidenceId",
                table: "Clues",
                column: "EvidenceId",
                principalTable: "Evidence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Suspects_KillerSuspectId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Evidence_Suspects_LinkedSuspectId",
                table: "Evidence");

            migrationBuilder.DropForeignKey(
                name: "FK_Clues_Cases_CaseId",
                table: "Clues");

            migrationBuilder.DropForeignKey(
                name: "FK_Evidence_Cases_CaseId",
                table: "Evidence");

            migrationBuilder.DropForeignKey(
                name: "FK_Clues_Evidence_EvidenceId",
                table: "Clues");

            migrationBuilder.DropTable(
                name: "PlayerProgress");

            migrationBuilder.DropTable(
                name: "PuzzleTestCases");

            migrationBuilder.DropTable(
                name: "Suspects");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Evidence");

            migrationBuilder.DropTable(
                name: "Clues");
        }
    }
}
