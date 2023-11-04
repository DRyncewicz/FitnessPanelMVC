using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPanelMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntityForUserReportFilesStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReportFile_AspNetUsers_UserId",
                table: "UserReportFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReportFile",
                table: "UserReportFile");

            migrationBuilder.RenameTable(
                name: "UserReportFile",
                newName: "UserReportFiles");

            migrationBuilder.RenameIndex(
                name: "IX_UserReportFile_UserId",
                table: "UserReportFiles",
                newName: "IX_UserReportFiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReportFiles",
                table: "UserReportFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReportFiles_AspNetUsers_UserId",
                table: "UserReportFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReportFiles_AspNetUsers_UserId",
                table: "UserReportFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReportFiles",
                table: "UserReportFiles");

            migrationBuilder.RenameTable(
                name: "UserReportFiles",
                newName: "UserReportFile");

            migrationBuilder.RenameIndex(
                name: "IX_UserReportFiles_UserId",
                table: "UserReportFile",
                newName: "IX_UserReportFile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReportFile",
                table: "UserReportFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReportFile_AspNetUsers_UserId",
                table: "UserReportFile",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
