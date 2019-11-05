using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ExreciseCorrectnessTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ProgrammingLanguages_ProgrammingLanguageId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ProgrammingLanguageId",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "ProgrammingLanguage",
                table: "Exercises",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestInputOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InputId = table.Column<Guid>(nullable: false),
                    OutputId = table.Column<Guid>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInputOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestInputOutputs_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestInputOutputs_TestInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "TestInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestInputOutputs_TestOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "TestOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestInputOutputs_ExerciseId",
                table: "TestInputOutputs",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInputOutputs_InputId",
                table: "TestInputOutputs",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInputOutputs_OutputId",
                table: "TestInputOutputs",
                column: "OutputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestInputOutputs");

            migrationBuilder.DropTable(
                name: "TestInputs");

            migrationBuilder.DropTable(
                name: "TestOutputs");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguage",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompilerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ProgrammingLanguageId",
                table: "Exercises",
                column: "ProgrammingLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ProgrammingLanguages_ProgrammingLanguageId",
                table: "Exercises",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
