using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockSchool.Data.Migrations
{
    /// <inheritdoc />
    public partial class After_DDD_Big_Bang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineEntityTeacherEntity");

            migrationBuilder.DropTable(
                name: "SubscriptionsAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherDisciplines",
                table: "TeacherDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_TeacherDisciplines_TeacherId",
                table: "TeacherDisciplines");

            migrationBuilder.DropColumn(
                name: "TeacherDisciplineId",
                table: "TeacherDisciplines");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Branches");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "WorkingPeriods",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Tenders",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "BranchId1",
                table: "Teachers",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Tariffs",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "StatusReason",
                table: "Subscriptions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Subscriptions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "Subscriptions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountOutstanding",
                table: "Subscriptions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsWaiting",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Schedules",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Schedules",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Schedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "ScheduledWorkingPeriods",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Rooms",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "BranchId1",
                table: "Rooms",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Roles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notes",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Notes",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Disciplines",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Disciplines",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Branches",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "Bands",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatusReason",
                table: "Attendances",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Attendances",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherDisciplines",
                table: "TeacherDisciplines",
                columns: new[] { "TeacherId", "DisciplineId" });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    SubscriptionAttendanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttendanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.SubscriptionAttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendees_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "AttendanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendees_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    PaidOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "RoomDisciplines",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDisciplines", x => new { x.RoomId, x.DisciplineId });
                    table.ForeignKey(
                        name: "FK_RoomDisciplines_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomDisciplines_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 1,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 2,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 3,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 4,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 5,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 6,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 7,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 8,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Disciplines",
                keyColumn: "DisciplineId",
                keyValue: 9,
                column: "TeacherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 10,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 11,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 12,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 13,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 14,
                columns: new[] { "BranchId1", "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { null, null, true, true });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriods_TeacherId1",
                table: "WorkingPeriods",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_BranchId1",
                table: "Teachers",
                column: "BranchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomId1",
                table: "Schedules",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkingPeriods_TeacherId1",
                table: "ScheduledWorkingPeriods",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchId1",
                table: "Rooms",
                column: "BranchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Bands_TeacherId1",
                table: "Bands",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_AttendanceId",
                table: "Attendees",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_SubscriptionId",
                table: "Attendees",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDisciplines_DisciplineId",
                table: "RoomDisciplines",
                column: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bands_Teachers_TeacherId1",
                table: "Bands",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId",
                table: "Disciplines",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Branches_BranchId1",
                table: "Rooms",
                column: "BranchId1",
                principalTable: "Branches",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkingPeriods_Teachers_TeacherId1",
                table: "ScheduledWorkingPeriods",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId1",
                table: "Schedules",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Branches_BranchId1",
                table: "Teachers",
                column: "BranchId1",
                principalTable: "Branches",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingPeriods_Teachers_TeacherId1",
                table: "WorkingPeriods",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bands_Teachers_TeacherId1",
                table: "Bands");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Branches_BranchId1",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkingPeriods_Teachers_TeacherId1",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId1",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Branches_BranchId1",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingPeriods_Teachers_TeacherId1",
                table: "WorkingPeriods");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RoomDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_WorkingPeriods_TeacherId1",
                table: "WorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_BranchId1",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherDisciplines",
                table: "TeacherDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_RoomId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledWorkingPeriods_TeacherId1",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BranchId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Bands_TeacherId1",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "WorkingPeriods");

            migrationBuilder.DropColumn(
                name: "BranchId1",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "ScheduledWorkingPeriods");

            migrationBuilder.DropColumn(
                name: "BranchId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Bands");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Tenders",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherDisciplineId",
                table: "TeacherDisciplines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Tariffs",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "StatusReason",
                table: "Subscriptions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Subscriptions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "Subscriptions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountOutstanding",
                table: "Subscriptions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsWaiting",
                table: "Students",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Schedules",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Schedules",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Roles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Notes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Disciplines",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Branches",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Branches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StatusReason",
                table: "Attendances",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Attendances",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherDisciplines",
                table: "TeacherDisciplines",
                column: "TeacherDisciplineId");

            migrationBuilder.CreateTable(
                name: "DisciplineEntityTeacherEntity",
                columns: table => new
                {
                    DisciplinesDisciplineId = table.Column<int>(type: "integer", nullable: false),
                    TeachersTeacherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineEntityTeacherEntity", x => new { x.DisciplinesDisciplineId, x.TeachersTeacherId });
                    table.ForeignKey(
                        name: "FK_DisciplineEntityTeacherEntity_Disciplines_DisciplinesDiscip~",
                        column: x => x.DisciplinesDisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineEntityTeacherEntity_Teachers_TeachersTeacherId",
                        column: x => x.TeachersTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionsAttendances",
                columns: table => new
                {
                    SubscriptionAttendanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttendanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StatusReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionsAttendances", x => x.SubscriptionAttendanceId);
                    table.ForeignKey(
                        name: "FK_SubscriptionsAttendances_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "AttendanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionsAttendances_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 1,
                column: "Phone",
                value: "77471237896");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 2,
                column: "Phone",
                value: "77471237896");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 10,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 11,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 12,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 13,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 14,
                columns: new[] { "Status", "SupportsRehearsal", "SupportsRent" },
                values: new object[] { 1, false, false });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDisciplines_TeacherId",
                table: "TeacherDisciplines",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineEntityTeacherEntity_TeachersTeacherId",
                table: "DisciplineEntityTeacherEntity",
                column: "TeachersTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsAttendances_AttendanceId",
                table: "SubscriptionsAttendances",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsAttendances_SubscriptionId",
                table: "SubscriptionsAttendances",
                column: "SubscriptionId");
        }
    }
}
