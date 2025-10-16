using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class GroupId_is_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "Attendances");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Attendances",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Attendances");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "Subscriptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "Attendances",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
