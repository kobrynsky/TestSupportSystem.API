using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedDetailsToExerciseResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseResults_Exercises_ExerciseId",
                table: "ExerciseResults");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseResults_ExerciseId",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "ExerciseResults");

            migrationBuilder.AddColumn<Guid>(
                name: "CorrectnessTestId",
                table: "ExerciseResults",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseResults_CorrectnessTestId",
                table: "ExerciseResults",
                column: "CorrectnessTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseResults_CorrectnessTests_CorrectnessTestId",
                table: "ExerciseResults",
                column: "CorrectnessTestId",
                principalTable: "CorrectnessTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseResults_CorrectnessTests_CorrectnessTestId",
                table: "ExerciseResults");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseResults_CorrectnessTestId",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "CorrectnessTestId",
                table: "ExerciseResults");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "ExerciseResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseResults_ExerciseId",
                table: "ExerciseResults",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseResults_Exercises_ExerciseId",
                table: "ExerciseResults",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
