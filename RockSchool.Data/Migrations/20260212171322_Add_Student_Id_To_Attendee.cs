using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Student_Id_To_Attendee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Branches_BranchId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Disciplines_DisciplineId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Rooms_RoomId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Teachers_TeacherId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Subscriptions_SubscriptionId",
                table: "Attendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendees",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_SubscriptionId",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_BranchId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_DisciplineId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_RoomId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_TeacherId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "SubscriptionAttendanceId",
                table: "Attendees",
                newName: "StudentId");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendeeId",
                table: "Attendees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendees",
                table: "Attendees",
                column: "AttendeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendees",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "AttendeeId",
                table: "Attendees");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Attendees",
                newName: "SubscriptionAttendanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendees",
                table: "Attendees",
                column: "SubscriptionAttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_SubscriptionId",
                table: "Attendees",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_BranchId",
                table: "Attendances",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DisciplineId",
                table: "Attendances",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_RoomId",
                table: "Attendances",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_TeacherId",
                table: "Attendances",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Branches_BranchId",
                table: "Attendances",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Disciplines_DisciplineId",
                table: "Attendances",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Rooms_RoomId",
                table: "Attendances",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Teachers_TeacherId",
                table: "Attendances",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Subscriptions_SubscriptionId",
                table: "Attendees",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "SubscriptionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
