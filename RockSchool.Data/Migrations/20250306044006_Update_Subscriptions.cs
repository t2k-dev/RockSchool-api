using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Subscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrial",
                table: "Subscriptions");

            migrationBuilder.Sql("ALTER TABLE public.\"Subscriptions\" ALTER COLUMN \"Status\" TYPE integer USING \"Status\"::integer");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceLength",
                table: "Subscriptions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceCount",
                table: "Subscriptions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TrialStatus",
                table: "Subscriptions",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrialStatus",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Subscriptions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceLength",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceCount",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrial",
                table: "Subscriptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
