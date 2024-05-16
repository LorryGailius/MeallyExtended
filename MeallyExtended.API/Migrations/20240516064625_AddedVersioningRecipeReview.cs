using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeallyExtended.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedVersioningRecipeReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Review",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Recipe",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Recipe");
        }
    }
}
