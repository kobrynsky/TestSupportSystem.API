using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedCorrectnessTestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseResults_CorrectnessTests_CorrectnessTestId",
                table: "ExerciseResults");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseResults_CorrectnessTestId",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "CompileOutput",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "CorrectnessTestId",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "Error",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "ExerciseResults");

            migrationBuilder.CreateTable(
                name: "CorrectnessTestResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseResultId = table.Column<Guid>(nullable: false),
                    CorrectnessTestId = table.Column<Guid>(nullable: false),
                    Time = table.Column<string>(nullable: true),
                    Memory = table.Column<int>(nullable: false),
                    CompileOutput = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Error = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectnessTestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectnessTestResult_CorrectnessTests_CorrectnessTestId",
                        column: x => x.CorrectnessTestId,
                        principalTable: "CorrectnessTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorrectnessTestResult_ExerciseResults_ExerciseResultId",
                        column: x => x.ExerciseResultId,
                        principalTable: "ExerciseResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectnessTestResult_CorrectnessTestId",
                table: "CorrectnessTestResult",
                column: "CorrectnessTestId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectnessTestResult_ExerciseResultId",
                table: "CorrectnessTestResult",
                column: "ExerciseResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectnessTestResult");

            migrationBuilder.AddColumn<string>(
                name: "CompileOutput",
                table: "ExerciseResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CorrectnessTestId",
                table: "ExerciseResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "ExerciseResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Memory",
                table: "ExerciseResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ExerciseResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ExerciseResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "ExerciseResults",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
