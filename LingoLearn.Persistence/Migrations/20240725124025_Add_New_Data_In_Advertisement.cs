using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_Data_In_Advertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Advertisements",
                newName: "ImagesUrl");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Advertisements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "ImagesUrl",
                table: "Advertisements",
                newName: "ImageUrl");
        }
    }
}
