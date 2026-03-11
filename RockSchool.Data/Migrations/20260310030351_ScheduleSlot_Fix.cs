using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleSlot_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleSlots_Rooms_RoomId1",
                table: "ScheduleSlots");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleSlots_RoomId1",
                table: "ScheduleSlots");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "ScheduleSlots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "ScheduleSlots",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSlots_RoomId1",
                table: "ScheduleSlots",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleSlots_Rooms_RoomId1",
                table: "ScheduleSlots",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }
    }
}
