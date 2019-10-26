using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ManyToManyCourseMainLecturers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseMainLecturers",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    MainLecturerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMainLecturers", x => new { x.CourseId, x.MainLecturerId });
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseMainLecturers_MainLecturerId",
                table: "CourseMainLecturers",
                column: "MainLecturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseMainLecturers");

        }
    }
}
