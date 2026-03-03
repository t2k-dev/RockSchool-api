using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Schedule_Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Bands_BandId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId1",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subscriptions_SubscriptionId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_BandId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_RoomId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubscriptionId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Schedules");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Schedules",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schedules",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Bands",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduleSlots",
                columns: table => new
                {
                    ScheduleSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    WeekDay = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSlots", x => x.ScheduleSlotId);
                    table.ForeignKey(
                        name: "FK_ScheduleSlots_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleSlots_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ScheduleId",
                table: "Subscriptions",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_ScheduleId",
                table: "Bands",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSlots_RoomId",
                table: "ScheduleSlots",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleSlots_ScheduleId",
                table: "ScheduleSlots",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_Schedules_ScheduleId",
                table: "Bands",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Schedules_ScheduleId",
                table: "Subscriptions",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bands_Schedules_ScheduleId",
                table: "Bands");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Schedules_ScheduleId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "ScheduleSlots");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ScheduleId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Bands_ScheduleId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Bands");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BandId",
                table: "Schedules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Schedules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Schedules",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Schedules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_BandId",
                table: "Schedules",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomId1",
                table: "Schedules",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubscriptionId",
                table: "Schedules",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Bands_BandId",
                table: "Schedules",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId1",
                table: "Schedules",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subscriptions_SubscriptionId",
                table: "Schedules",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "SubscriptionId");
        }
    }
}
