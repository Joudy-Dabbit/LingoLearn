using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Advertisements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Advertisements",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Advertisements",
                newName: "Name");
        }
    }
}
