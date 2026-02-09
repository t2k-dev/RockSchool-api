using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Link_For_Scheduled_Working_Periods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropColumn(
                name: "WorkingPeriodId",
                table: "ScheduledWorkingPeriods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodId",
                principalTable: "WorkingPeriods",
                principalColumn: "WorkingPeriodId");
        }
    }
}
