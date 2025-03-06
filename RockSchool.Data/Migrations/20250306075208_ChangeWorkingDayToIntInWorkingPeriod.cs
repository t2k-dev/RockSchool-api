using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWorkingDayToIntInWorkingPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"WorkingPeriods\" ALTER COLUMN \"WeekDay\" TYPE integer USING \"WeekDay\"::integer;");
            // migrationBuilder.AlterColumn<int>(
            //     name: "WeekDay",
            //     table: "WorkingPeriods",
            //     type: "integer",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"WorkingPeriods\" ALTER COLUMN \"WeekDay\" TYPE text USING \"WeekDay\"::text;");
            // migrationBuilder.AlterColumn<string>(
            //     name: "WeekDay",
            //     table: "WorkingPeriods",
            //     type: "text",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "integer");
        }
    }
}
