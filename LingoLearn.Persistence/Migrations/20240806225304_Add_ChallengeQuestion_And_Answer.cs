using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ChallengeQuestion_And_Answer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsMultiChoices = table.Column<bool>(type: "bit", nullable: false),
                    ChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtcDateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UtcDateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UtcDateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeQuestion_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtcDateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UtcDateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UtcDateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeAnswer_ChallengeQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ChallengeQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeAnswer_QuestionId",
                table: "ChallengeAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeQuestion_ChallengeId",
                table: "ChallengeQuestion",
                column: "ChallengeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeAnswer");

            migrationBuilder.DropTable(
                name: "ChallengeQuestion");
        }
    }
}
