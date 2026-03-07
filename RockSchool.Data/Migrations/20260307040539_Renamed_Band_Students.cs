using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Band_Students : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandStudents_Bands_BandId",
                table: "BandStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_BandStudents_Students_StudentId",
                table: "BandStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandStudents",
                table: "BandStudents");

            migrationBuilder.RenameTable(
                name: "BandStudents",
                newName: "BandMembers");

            migrationBuilder.RenameIndex(
                name: "IX_BandStudents_StudentId",
                table: "BandMembers",
                newName: "IX_BandMembers_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_BandStudents_BandId",
                table: "BandMembers",
                newName: "IX_BandMembers_BandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers",
                column: "BandStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Bands_BandId",
                table: "BandMembers",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Students_StudentId",
                table: "BandMembers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Bands_BandId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Students_StudentId",
                table: "BandMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers");

            migrationBuilder.RenameTable(
                name: "BandMembers",
                newName: "BandStudents");

            migrationBuilder.RenameIndex(
                name: "IX_BandMembers_StudentId",
                table: "BandStudents",
                newName: "IX_BandStudents_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_BandMembers_BandId",
                table: "BandStudents",
                newName: "IX_BandStudents_BandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandStudents",
                table: "BandStudents",
                column: "BandStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BandStudents_Bands_BandId",
                table: "BandStudents",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BandStudents_Students_StudentId",
                table: "BandStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
