using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_RoomId_To_WorkingPeriods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "WorkingPeriods",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "ScheduledWorkingPeriods",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriods_RoomId",
                table: "WorkingPeriods",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_RoomId",
                table: "ScheduledWorkingPeriods",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_Rooms_RoomId",
                table: "ScheduledWorkingPeriods",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriods_Rooms_RoomId",
                table: "WorkingPeriods",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_Rooms_RoomId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriods_Rooms_RoomId",
                table: "WorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriods_RoomId",
                table: "WorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_RoomId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "WorkingPeriods");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "ScheduledWorkingPeriods");
        }
    }
}
