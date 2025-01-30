using Microsoft.EntityFrameworkCore.Migrations;

namespace RockSchool.WebApi.Migrations
{
    public partial class StudentModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "StudentService");

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "Teachers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "StudentService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<short>(
                name: "Sex",
                table: "StudentService",
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
                table: "StudentService");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "StudentService");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "StudentService",
                type: "text",
                nullable: true);
        }
    }
}
