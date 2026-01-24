using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Attendance_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrial",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceType",
                table: "Attendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceType",
                table: "Attendances");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrial",
                table: "Attendances",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
