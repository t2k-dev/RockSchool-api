using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_BandId_To_Schedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BandId",
                table: "Schedules",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_BandId",
                table: "Schedules",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Bands_BandId",
                table: "Schedules",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Bands_BandId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_BandId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Schedules");
        }
    }
}
