using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Languages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_StudentId",
                table: "Languages",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_StudentId",
                table: "Languages",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_StudentId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_StudentId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImagUrl",
                table: "AspNetUsers");
        }
    }
}
