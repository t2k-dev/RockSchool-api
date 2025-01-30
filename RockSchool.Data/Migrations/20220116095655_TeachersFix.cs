using Microsoft.EntityFrameworkCore.Migrations;

namespace RockSchool.WebApi.Migrations
{
    public partial class TeachersFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplineTeacher_Disciplines_DisciplineId",
                table: "DisciplineTeacher");

            migrationBuilder.RenameColumn(
                name: "DisciplineId",
                table: "DisciplineTeacher",
                newName: "DisciplinesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineTeacher_Disciplines_DisciplinesId",
                table: "DisciplineTeacher",
                column: "DisciplinesId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplineTeacher_Disciplines_DisciplinesId",
                table: "DisciplineTeacher");

            migrationBuilder.RenameColumn(
                name: "DisciplinesId",
                table: "DisciplineTeacher",
                newName: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineTeacher_Disciplines_DisciplineId",
                table: "DisciplineTeacher",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
