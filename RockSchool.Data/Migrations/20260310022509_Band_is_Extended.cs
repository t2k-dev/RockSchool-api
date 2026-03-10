using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Band_is_Extended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bands_Teachers_TeacherId1",
                table: "Bands");

            migrationBuilder.DropIndex(
                name: "IX_Bands_TeacherId1",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Bands");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Bands",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BandId",
                table: "Attendances",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_BranchId",
                table: "Bands",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_Branches_BranchId",
                table: "Bands",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bands_Branches_BranchId",
                table: "Bands");

            migrationBuilder.DropIndex(
                name: "IX_Bands_BranchId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Attendances");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "Bands",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bands_TeacherId1",
                table: "Bands",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_Teachers_TeacherId1",
                table: "Bands",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }
    }
}
