using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduledWorkingPeriods_link_To_Teacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_TeacherId",
                table: "ScheduledWorkingPeriods",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_Teachers_TeacherId",
                table: "ScheduledWorkingPeriods",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_Teachers_TeacherId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_TeacherId",
                table: "ScheduledWorkingPeriods");
        }
    }
}
