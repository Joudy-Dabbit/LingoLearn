using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_StudentLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLesson_AspNetUsers_StudentId",
                table: "StudentLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLesson_Lessons_LessonId",
                table: "StudentLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLesson",
                table: "StudentLesson");

            migrationBuilder.RenameTable(
                name: "StudentLesson",
                newName: "StudentLessons");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLesson_StudentId",
                table: "StudentLessons",
                newName: "IX_StudentLessons_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLesson_LessonId",
                table: "StudentLessons",
                newName: "IX_StudentLessons_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLessons",
                table: "StudentLessons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessons_AspNetUsers_StudentId",
                table: "StudentLessons",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLessons_Lessons_LessonId",
                table: "StudentLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessons_AspNetUsers_StudentId",
                table: "StudentLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLessons_Lessons_LessonId",
                table: "StudentLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentLessons",
                table: "StudentLessons");

            migrationBuilder.RenameTable(
                name: "StudentLessons",
                newName: "StudentLesson");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessons_StudentId",
                table: "StudentLesson",
                newName: "IX_StudentLesson_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentLessons_LessonId",
                table: "StudentLesson",
                newName: "IX_StudentLesson_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentLesson",
                table: "StudentLesson",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLesson_AspNetUsers_StudentId",
                table: "StudentLesson",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLesson_Lessons_LessonId",
                table: "StudentLesson",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
