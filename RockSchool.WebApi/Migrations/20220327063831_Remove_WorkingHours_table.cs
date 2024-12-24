using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RockSchool.WebApi.Migrations
{
    public partial class Remove_WorkingHours_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingPeriods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingPeriods",
                columns: table => new
                {
                    WorkingPeriodId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    WeekDay = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriods", x => x.WorkingPeriodId);
                    table.ForeignKey(
                        name: "FK_WorkingPeriods_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriods_TeacherId",
                table: "WorkingPeriods",
                column: "TeacherId");
        }
    }
}
