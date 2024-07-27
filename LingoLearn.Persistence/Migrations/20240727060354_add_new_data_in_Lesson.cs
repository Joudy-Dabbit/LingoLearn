using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_new_data_in_Lesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpectedTimeOfCompletionInMinute",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Links",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedTimeOfCompletionInMinute",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Links",
                table: "Lessons");
        }
    }
}
