using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPanelMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertiesForMealsAndRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCarbs",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFat",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalProtein",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCarbs",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFat",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalProtein",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TotalCarbs",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TotalFat",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TotalProtein",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalCarbs",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalFat",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalProtein",
                table: "Meals");
        }
    }
}
