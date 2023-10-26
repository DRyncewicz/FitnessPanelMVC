using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPanelMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMealProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MealProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MealProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
