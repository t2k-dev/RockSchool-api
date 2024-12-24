using Microsoft.EntityFrameworkCore.Migrations;

namespace RockSchool.WebApi.Migrations
{
    public partial class StudentModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Students");

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "Teachers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<short>(
                name: "Sex",
                table: "Students",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Students",
                type: "text",
                nullable: true);
        }
    }
}
