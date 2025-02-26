using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStudentLevelToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<int>(
            //     name: "Level",
            //     table: "Students",
            //     type: "integer",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            
            migrationBuilder.Sql("ALTER TABLE \"Students\" ALTER COLUMN \"Level\" TYPE integer USING \"Level\"::integer;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<string>(
            //     name: "Level",
            //     table: "Students",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(int),
            //     oldType: "integer",
            //     oldNullable: true);
            
            migrationBuilder.Sql("ALTER TABLE \"Students\" ALTER COLUMN \"Level\" TYPE text USING \"Level\"::text;");
        }
    }
}
