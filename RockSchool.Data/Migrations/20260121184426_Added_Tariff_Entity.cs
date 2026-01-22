using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Tariff_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    TariffId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: true),
                    AttendanceLength = table.Column<int>(type: "integer", nullable: false),
                    AttendanceCount = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.TariffId);
                    table.ForeignKey(
                        name: "FK_Tariffs_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_DisciplineId",
                table: "Tariffs",
                column: "DisciplineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tariffs");
        }
    }
}
