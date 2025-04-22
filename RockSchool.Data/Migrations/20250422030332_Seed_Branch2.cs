using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Branch2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "Address", "Name", "Phone" },
                values: new object[] { 2, "Аль-Фараби 15", "На Аль-Фараби", "77471237896" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "BranchId", "IsActive", "Name", "Status" },
                values: new object[,]
                {
                    { 10, 2, true, "Гитарная", 1 },
                    { 11, 2, true, "Вокальная", 1 },
                    { 12, 2, true, "Барабанная", 1 },
                    { 13, 2, true, "Плакатная", 1 },
                    { 14, 2, true, "Желтая", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 2);
        }
    }
}
