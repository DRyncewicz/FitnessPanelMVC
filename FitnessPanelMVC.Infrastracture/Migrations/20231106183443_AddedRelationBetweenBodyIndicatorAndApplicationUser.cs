using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPanelMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationBetweenBodyIndicatorAndApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BodyIndicators",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BodyIndicators_UserId",
                table: "BodyIndicators",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyIndicators_AspNetUsers_UserId",
                table: "BodyIndicators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyIndicators_AspNetUsers_UserId",
                table: "BodyIndicators");

            migrationBuilder.DropIndex(
                name: "IX_BodyIndicators_UserId",
                table: "BodyIndicators");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BodyIndicators");
        }
    }
}
