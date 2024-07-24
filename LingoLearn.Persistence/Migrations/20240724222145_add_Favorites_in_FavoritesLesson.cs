using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_Favorites_in_FavoritesLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LessonId1",
                table: "FavoriteLessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLessons_LessonId1",
                table: "FavoriteLessons",
                column: "LessonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLessons_Lessons_LessonId1",
                table: "FavoriteLessons",
                column: "LessonId1",
                principalTable: "Lessons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLessons_Lessons_LessonId1",
                table: "FavoriteLessons");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteLessons_LessonId1",
                table: "FavoriteLessons");

            migrationBuilder.DropColumn(
                name: "LessonId1",
                table: "FavoriteLessons");
        }
    }
}
