using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchToStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_BranchId",
                table: "Students",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Branches_BranchId",
                table: "Students",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Branches_BranchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_BranchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Students");
        }
    }
}
