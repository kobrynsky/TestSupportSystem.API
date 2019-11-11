using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedCorrectnessTestResult2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectnessTestResult_CorrectnessTests_CorrectnessTestId",
                table: "CorrectnessTestResult");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectnessTestResult_ExerciseResults_ExerciseResultId",
                table: "CorrectnessTestResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorrectnessTestResult",
                table: "CorrectnessTestResult");

            migrationBuilder.RenameTable(
                name: "CorrectnessTestResult",
                newName: "CorrectnessTestResults");

            migrationBuilder.RenameIndex(
                name: "IX_CorrectnessTestResult_ExerciseResultId",
                table: "CorrectnessTestResults",
                newName: "IX_CorrectnessTestResults_ExerciseResultId");

            migrationBuilder.RenameIndex(
                name: "IX_CorrectnessTestResult_CorrectnessTestId",
                table: "CorrectnessTestResults",
                newName: "IX_CorrectnessTestResults_CorrectnessTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorrectnessTestResults",
                table: "CorrectnessTestResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectnessTestResults_CorrectnessTests_CorrectnessTestId",
                table: "CorrectnessTestResults",
                column: "CorrectnessTestId",
                principalTable: "CorrectnessTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectnessTestResults_ExerciseResults_ExerciseResultId",
                table: "CorrectnessTestResults",
                column: "ExerciseResultId",
                principalTable: "ExerciseResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectnessTestResults_CorrectnessTests_CorrectnessTestId",
                table: "CorrectnessTestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectnessTestResults_ExerciseResults_ExerciseResultId",
                table: "CorrectnessTestResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorrectnessTestResults",
                table: "CorrectnessTestResults");

            migrationBuilder.RenameTable(
                name: "CorrectnessTestResults",
                newName: "CorrectnessTestResult");

            migrationBuilder.RenameIndex(
                name: "IX_CorrectnessTestResults_ExerciseResultId",
                table: "CorrectnessTestResult",
                newName: "IX_CorrectnessTestResult_ExerciseResultId");

            migrationBuilder.RenameIndex(
                name: "IX_CorrectnessTestResults_CorrectnessTestId",
                table: "CorrectnessTestResult",
                newName: "IX_CorrectnessTestResult_CorrectnessTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorrectnessTestResult",
                table: "CorrectnessTestResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectnessTestResult_CorrectnessTests_CorrectnessTestId",
                table: "CorrectnessTestResult",
                column: "CorrectnessTestId",
                principalTable: "CorrectnessTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectnessTestResult_ExerciseResults_ExerciseResultId",
                table: "CorrectnessTestResult",
                column: "ExerciseResultId",
                principalTable: "ExerciseResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
