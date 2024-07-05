using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Student_in_Reply_and_Comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Lessons_LessonId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_LessonId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Replies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "Replies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Replies_LessonId",
                table: "Replies",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Lessons_LessonId",
                table: "Replies",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
