using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDisciplineTeacherId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Disciplines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Disciplines",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 1,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 2,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 3,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 4,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 5,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 6,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 7,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 8,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 9,
                column: "TeacherId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId",
                table: "Disciplines",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }
    }
}
