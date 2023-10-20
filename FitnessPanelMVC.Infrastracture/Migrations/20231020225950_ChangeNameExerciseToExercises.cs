using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessPanelMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameExerciseToExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_Exercise_ExerciseId",
                table: "WorkoutExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_Exercises_ExerciseId",
                table: "WorkoutExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_Exercises_ExerciseId",
                table: "WorkoutExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_Exercise_ExerciseId",
                table: "WorkoutExercise",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
