using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class MiddleName_is_Dropped_For_Teacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Teachers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Teachers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
