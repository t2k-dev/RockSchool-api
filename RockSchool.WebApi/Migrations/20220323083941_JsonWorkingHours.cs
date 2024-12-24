using Microsoft.EntityFrameworkCore.Migrations;

namespace RockSchool.WebApi.Migrations
{
    public partial class JsonWorkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                table: "Teachers",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Teachers");
        }
    }
}
