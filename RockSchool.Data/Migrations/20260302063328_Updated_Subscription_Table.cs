using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Subscription_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrialStatus",
                table: "Subscriptions",
                newName: "TrialDecision");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseSubscriptionId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSubscriptionId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "TrialDecision",
                table: "Subscriptions",
                newName: "TrialStatus");
        }
    }
}
