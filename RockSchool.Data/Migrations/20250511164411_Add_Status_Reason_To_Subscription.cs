using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Status_Reason_To_Subscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusReason",
                table: "Subscriptions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusReason",
                table: "Subscriptions");
        }
    }
}
