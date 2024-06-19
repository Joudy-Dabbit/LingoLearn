using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_LevelId_to_Certificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "Certificates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_LevelId",
                table: "Certificates",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Levels_LevelId",
                table: "Certificates",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Levels_LevelId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_LevelId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Certificates");
        }
    }
}
