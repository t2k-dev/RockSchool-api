using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduledWorkingPeriodEntityNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodEntityW~",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodEntityWorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropColumn(
                name: "WorkingPeriodEntityWorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodId",
                principalTable: "WorkingPeriods",
                principalColumn: "WorkingPeriodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkingPeriodEntityWorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodEntityWorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodEntityWorkingPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodEntityW~",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodEntityWorkingPeriodId",
                principalTable: "WorkingPeriods",
                principalColumn: "WorkingPeriodId");
        }
    }
}
