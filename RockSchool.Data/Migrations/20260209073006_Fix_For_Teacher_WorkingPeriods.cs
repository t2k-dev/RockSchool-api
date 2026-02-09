using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_For_Teacher_WorkingPeriods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriods_Teachers_TeacherId1",
                table: "WorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriods_TeacherId1",
                table: "WorkingPeriods");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "WorkingPeriods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "WorkingPeriods",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriods_TeacherId1",
                table: "WorkingPeriods",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriods_Teachers_TeacherId1",
                table: "WorkingPeriods",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }
    }
}
