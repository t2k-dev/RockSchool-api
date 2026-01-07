using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Discipline_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Disciplines_DisciplineId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Teachers_TeacherId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "Subscriptions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Disciplines_DisciplineId",
                table: "Subscriptions",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Teachers_TeacherId",
                table: "Subscriptions",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Disciplines_DisciplineId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Teachers_TeacherId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Subscriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Disciplines_DisciplineId",
                table: "Subscriptions",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Teachers_TeacherId",
                table: "Subscriptions",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
