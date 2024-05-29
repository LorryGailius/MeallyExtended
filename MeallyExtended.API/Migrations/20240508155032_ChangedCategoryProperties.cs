using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeallyExtended.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCategoryProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipe_Category_CategoriesId",
                table: "CategoryRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "CategoryRecipe");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "ClickCount",
                table: "RecipeLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoriesName",
                table: "CategoryRecipe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe",
                columns: new[] { "CategoriesName", "RecipesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipe_Category_CategoriesName",
                table: "CategoryRecipe",
                column: "CategoriesName",
                principalTable: "Category",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipe_Category_CategoriesName",
                table: "CategoryRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ClickCount",
                table: "RecipeLikes");

            migrationBuilder.DropColumn(
                name: "CategoriesName",
                table: "CategoryRecipe");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriesId",
                table: "CategoryRecipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe",
                columns: new[] { "CategoriesId", "RecipesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipe_Category_CategoriesId",
                table: "CategoryRecipe",
                column: "CategoriesId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
