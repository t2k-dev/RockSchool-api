using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding_New_Tariffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "TariffId", "Amount", "AttendanceCount", "AttendanceLength", "DisciplineId", "EndDate", "StartDate", "SubscriptionType" },
                values: new object[,]
                {
                    { new Guid("107f43b0-46c0-4b5f-a6e0-58658a4d0aa8"), 2000m, 1, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("1605538b-4e77-4dda-a492-44040dee1ea0"), 9000m, 1, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("1916fc99-d9d4-4b48-a30a-07f2b8271224"), 30000m, 4, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("21a7e82a-b0fd-4e34-8d01-cce816ed34ba"), 54000m, 8, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("24e75034-ebed-44e7-a44c-ea12e33a6767"), 74000m, 12, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("2c45b82e-16dc-4bf2-bd78-eb5922cbe3ec"), 11000m, 1, 1, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("32cfcefc-803e-4922-b358-6a9add488aab"), 42000m, 4, 1, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("3b5a0821-8663-4e1b-8fcc-b48c03d97185"), 69000m, 8, 1, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("6ee39316-06f7-4c1b-9167-f35c3dce5f80"), 96000m, 12, 1, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("83a26ae5-6cda-4877-95ab-ddca0b064578"), 24000m, 4, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("8d9e7ed0-9dcf-4686-bbf3-744f804d0826"), 36000m, 8, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("a57918be-0f1c-41c6-bc2d-add554ac2969"), 35000m, 4, 1, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("adcf2ba3-588c-46d8-bab6-04b3a54c2a1d"), 59000m, 8, 1, 9, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("af7b2695-618a-4cf3-b57b-14e6aadc0187"), 24000m, 4, 3, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { new Guid("becf83b9-bb78-4130-a654-c17c2f7a869a"), 3000m, 1, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { new Guid("ddf4bb6c-686a-490e-9440-61c5e2e68513"), 11000m, 4, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { new Guid("eaa18e35-d9a5-41b0-946b-7557fdc199a8"), 20000m, 8, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { new Guid("f79be13e-77ae-4728-b1ce-6e922c9b066d"), 30000m, 12, 1, null, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("107f43b0-46c0-4b5f-a6e0-58658a4d0aa8"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("1605538b-4e77-4dda-a492-44040dee1ea0"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("1916fc99-d9d4-4b48-a30a-07f2b8271224"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("21a7e82a-b0fd-4e34-8d01-cce816ed34ba"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("24e75034-ebed-44e7-a44c-ea12e33a6767"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("2c45b82e-16dc-4bf2-bd78-eb5922cbe3ec"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("32cfcefc-803e-4922-b358-6a9add488aab"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("3b5a0821-8663-4e1b-8fcc-b48c03d97185"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("6ee39316-06f7-4c1b-9167-f35c3dce5f80"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("83a26ae5-6cda-4877-95ab-ddca0b064578"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("8d9e7ed0-9dcf-4686-bbf3-744f804d0826"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("a57918be-0f1c-41c6-bc2d-add554ac2969"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("adcf2ba3-588c-46d8-bab6-04b3a54c2a1d"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("af7b2695-618a-4cf3-b57b-14e6aadc0187"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("becf83b9-bb78-4130-a654-c17c2f7a869a"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("ddf4bb6c-686a-490e-9440-61c5e2e68513"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("eaa18e35-d9a5-41b0-946b-7557fdc199a8"));

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "TariffId",
                keyValue: new Guid("f79be13e-77ae-4728-b1ce-6e922c9b066d"));
        }
    }
}
