using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class dumb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Branches_BranchEntityBranchId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BranchEntityBranchId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BranchEntityBranchId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchEntityBranchId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchEntityBranchId",
                table: "Users",
                column: "BranchEntityBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Branches_BranchEntityBranchId",
                table: "Users",
                column: "BranchEntityBranchId",
                principalTable: "Branches",
                principalColumn: "BranchId");
        }
    }
}
