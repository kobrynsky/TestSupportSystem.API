using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Persistence.Migrations
{
    public partial class AddedGroupidColumnToExerciseResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "ExerciseResults",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseResults_GroupId",
                table: "ExerciseResults",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseResults_Groups_GroupId",
                table: "ExerciseResults",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseResults_Groups_GroupId",
                table: "ExerciseResults");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseResults_GroupId",
                table: "ExerciseResults");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ExerciseResults");
        }
    }
}
