using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Nullable_For_WorkingPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodId",
                principalTable: "WorkingPeriods",
                principalColumn: "WorkingPeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodId",
                principalTable: "WorkingPeriods",
                principalColumn: "WorkingPeriodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
