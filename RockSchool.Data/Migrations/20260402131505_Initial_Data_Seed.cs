using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Data_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Абая 137", "На Абая" },
                    { 2, "Аль-Фараби 15", "На Аль-Фараби" }
                });

            migrationBuilder.InsertData(
                table: "Disciplines",
                columns: new[] { "DisciplineId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Guitar" },
                    { 2, true, "Electric Guitar" },
                    { 3, true, "Bass Guitar" },
                    { 4, true, "Ukulele" },
                    { 5, true, "Vocal" },
                    { 6, true, "Drums" },
                    { 7, true, "Piano" },
                    { 8, true, "Violin" },
                    { 9, true, "Extreme Vocal" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsActive", "RoleName" },
                values: new object[,]
                {
                    { 1, true, "Admin" },
                    { 2, true, "Teacher" },
                    { 3, true, "Student" }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "TariffId", "Amount", "AttendanceCount", "AttendanceLength", "DisciplineId", "EndDate", "StartDate", "SubscriptionType" },
                values: new object[,]
                {
                    { new Guid("107f43b0-46c0-4b5f-a6e0-58658a4d0aa8"), 2000m, 1, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("1605538b-4e77-4dda-a492-44040dee1ea0"), 9000m, 1, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("1916fc99-d9d4-4b48-a30a-07f2b8271224"), 30000m, 4, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("21a7e82a-b0fd-4e34-8d01-cce816ed34ba"), 54000m, 8, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("24e75034-ebed-44e7-a44c-ea12e33a6767"), 74000m, 12, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("83a26ae5-6cda-4877-95ab-ddca0b064578"), 24000m, 4, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("8d9e7ed0-9dcf-4686-bbf3-744f804d0826"), 36000m, 8, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("af7b2695-618a-4cf3-b57b-14e6aadc0187"), 24000m, 4, 120, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { new Guid("becf83b9-bb78-4130-a654-c17c2f7a869a"), 3000m, 1, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { new Guid("ddf4bb6c-686a-490e-9440-61c5e2e68513"), 11000m, 4, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { new Guid("eaa18e35-d9a5-41b0-946b-7557fdc199a8"), 20000m, 8, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { new Guid("f79be13e-77ae-4728-b1ce-6e922c9b066d"), 30000m, 12, 60, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "BranchId", "IsActive", "Name", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[,]
                {
                    { 1, 1, true, "Гитарная", null, true, true },
                    { 2, 1, true, "Вокальная", null, true, true },
                    { 4, 1, true, "Барабанная", null, true, true },
                    { 5, 1, true, "Желтая", null, true, true },
                    { 6, 1, true, "Зелёная", null, true, true },
                    { 10, 2, true, "Гитарная", null, true, true },
                    { 11, 2, true, "Вокальная", null, true, true },
                    { 12, 2, true, "Барабанная", null, true, true },
                    { 13, 2, true, "Плакатная", null, true, true },
                    { 14, 2, true, "Желтая", null, true, true }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "TariffId", "Amount", "AttendanceCount", "AttendanceLength", "DisciplineId", "EndDate", "StartDate", "SubscriptionType" },
                values: new object[,]
                {
                    { new Guid("2c45b82e-16dc-4bf2-bd78-eb5922cbe3ec"), 11000m, 1, 60, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("32cfcefc-803e-4922-b358-6a9add488aab"), 42000m, 4, 60, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("3b5a0821-8663-4e1b-8fcc-b48c03d97185"), 69000m, 8, 60, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("6ee39316-06f7-4c1b-9167-f35c3dce5f80"), 96000m, 12, 60, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("a57918be-0f1c-41c6-bc2d-add554ac2969"), 35000m, 4, 60, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("adcf2ba3-588c-46d8-bab6-04b3a54c2a1d"), 59000m, 8, 60, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
