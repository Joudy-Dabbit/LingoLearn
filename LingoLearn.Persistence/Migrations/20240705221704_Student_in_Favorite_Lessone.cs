using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Student_in_Favorite_Lessone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLessons_AspNetUsers_UserId",
                table: "FavoriteLessons");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteLessons",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteLessons_UserId",
                table: "FavoriteLessons",
                newName: "IX_FavoriteLessons_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLessons_AspNetUsers_StudentId",
                table: "FavoriteLessons",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLessons_AspNetUsers_StudentId",
                table: "FavoriteLessons");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "FavoriteLessons",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteLessons_StudentId",
                table: "FavoriteLessons",
                newName: "IX_FavoriteLessons_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLessons_AspNetUsers_UserId",
                table: "FavoriteLessons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
