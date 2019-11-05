using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ExreciseCorrectnessTests3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestInputOutputs");

            migrationBuilder.DropTable(
                name: "TestInputs");

            migrationBuilder.DropTable(
                name: "TestOutputs");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageId",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "CorrectnessTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectnessTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectnessTests_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectnessTestInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CorrectnessTestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectnessTestInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectnessTestInputs_CorrectnessTests_CorrectnessTestId",
                        column: x => x.CorrectnessTestId,
                        principalTable: "CorrectnessTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectnessTestOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CorrectnessTestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectnessTestOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrectnessTestOutputs_CorrectnessTests_CorrectnessTestId",
                        column: x => x.CorrectnessTestId,
                        principalTable: "CorrectnessTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrectnessTestInputs_CorrectnessTestId",
                table: "CorrectnessTestInputs",
                column: "CorrectnessTestId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectnessTestOutputs_CorrectnessTestId",
                table: "CorrectnessTestOutputs",
                column: "CorrectnessTestId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectnessTests_ExerciseId",
                table: "CorrectnessTests",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectnessTestInputs");

            migrationBuilder.DropTable(
                name: "CorrectnessTestOutputs");

            migrationBuilder.DropTable(
                name: "CorrectnessTests");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgrammingLanguageId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TestInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestInputOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
    }
}
