using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class GradesAndMainLecturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseUsers_AspNetUsers_UserId",
                table: "ExerciseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseUsers",
                table: "ExerciseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseUsers_UserId",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExerciseUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ExerciseUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAssesment",
                table: "ExerciseUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "ExerciseUsers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "ExerciseUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LecturerId",
                table: "ExerciseUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "ExerciseUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainLecturerId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseUsers",
                table: "ExerciseUsers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CourseMainLecturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MainLecturerId = table.Column<string>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMainLecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseMainLecturers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseMainLecturers_AspNetUsers_MainLecturerId",
                        column: x => x.MainLecturerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUsers_ExerciseId",
                table: "ExerciseUsers",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUsers_GroupId",
                table: "ExerciseUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUsers_LecturerId",
                table: "ExerciseUsers",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUsers_StudentId",
                table: "ExerciseUsers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_AuthorId",
                table: "Exercises",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MainLecturerId",
                table: "Courses",
                column: "MainLecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMainLecturers_CourseId",
                table: "CourseMainLecturers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMainLecturers_MainLecturerId",
                table: "CourseMainLecturers",
                column: "MainLecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_MainLecturerId",
                table: "Courses",
                column: "MainLecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_AuthorId",
                table: "Exercises",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseUsers_Groups_GroupId",
                table: "ExerciseUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseUsers_AspNetUsers_LecturerId",
                table: "ExerciseUsers",
                column: "LecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseUsers_AspNetUsers_StudentId",
                table: "ExerciseUsers",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_MainLecturerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_AuthorId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseUsers_Groups_GroupId",
                table: "ExerciseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseUsers_AspNetUsers_LecturerId",
                table: "ExerciseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseUsers_AspNetUsers_StudentId",
                table: "ExerciseUsers");

            migrationBuilder.DropTable(
                name: "CourseMainLecturers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseUsers",
                table: "ExerciseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseUsers_ExerciseId",
                table: "ExerciseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseUsers_GroupId",
                table: "ExerciseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseUsers_LecturerId",
                table: "ExerciseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseUsers_StudentId",
                table: "ExerciseUsers");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_AuthorId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MainLecturerId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "DateOfAssesment",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ExerciseUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MainLecturerId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ExerciseUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseUsers",
                table: "ExerciseUsers",
                columns: new[] { "ExerciseId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseUsers_UserId",
                table: "ExerciseUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseUsers_AspNetUsers_UserId",
                table: "ExerciseUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
