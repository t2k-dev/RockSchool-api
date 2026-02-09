using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_For_Scheduled_Wopking_Periods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_Rooms_RoomId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_Teachers_TeacherId1",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_RoomId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_TeacherId1",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "ScheduledWorkingPeriods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "ScheduledWorkingPeriods",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_RoomId",
                table: "ScheduledWorkingPeriods",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_TeacherId1",
                table: "ScheduledWorkingPeriods",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_Rooms_RoomId",
                table: "ScheduledWorkingPeriods",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_Teachers_TeacherId1",
                table: "ScheduledWorkingPeriods",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }
    }
}
