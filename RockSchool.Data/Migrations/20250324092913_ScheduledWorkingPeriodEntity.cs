using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScheduledWorkingPeriodEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledWorkingPeriods",
                columns: table => new
                {
                    ScheduledWorkingPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkingPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkingPeriodEntityWorkingPeriodId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledWorkingPeriods", x => x.ScheduledWorkingPeriodId);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkingPeriods_WorkingPeriods_WorkingPeriodEntityW~",
                        column: x => x.WorkingPeriodEntityWorkingPeriodId,
                        principalTable: "WorkingPeriods",
                        principalColumn: "WorkingPeriodId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_WorkingPeriodEntityWorkingPeriodId",
                table: "ScheduledWorkingPeriods",
                column: "WorkingPeriodEntityWorkingPeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledWorkingPeriods");
        }
    }
}
