using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class BranchId_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Branches_BranchId1",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Branches_BranchId1",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_BranchId1",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BranchId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BranchId1",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "BranchId1",
                table: "Rooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId1",
                table: "Teachers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId1",
                table: "Rooms",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 10,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 11,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 12,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 13,
                column: "BranchId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 14,
                column: "BranchId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_BranchId1",
                table: "Teachers",
                column: "BranchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchId1",
                table: "Rooms",
                column: "BranchId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Branches_BranchId1",
                table: "Rooms",
                column: "BranchId1",
                principalTable: "Branches",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Branches_BranchId1",
                table: "Teachers",
                column: "BranchId1",
                principalTable: "Branches",
                principalColumn: "BranchId");
        }
    }
}
