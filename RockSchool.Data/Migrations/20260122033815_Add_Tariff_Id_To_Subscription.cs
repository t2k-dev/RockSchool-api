using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Tariff_Id_To_Subscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TariffId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TariffId",
                table: "Subscriptions",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tariffs_TariffId",
                table: "Subscriptions",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "TariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tariffs_TariffId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_TariffId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "Subscriptions");
        }
    }
}
