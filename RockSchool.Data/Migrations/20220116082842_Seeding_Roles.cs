using Microsoft.EntityFrameworkCore.Migrations;

namespace RockSchool.WebApi.Migrations
{
    public partial class Seeding_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_User_UserId",
                table: "StudentService");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_User_UserId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "UserService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "UserService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "RoleEntity");

            migrationBuilder.RenameTable(
                name: "UserService",
                newName: "UserService");

            migrationBuilder.RenameTable(
                name: "RoleEntity",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "UserService",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserService",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "UserService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "TeacherEntity" },
                    { 3, "StudentService" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "StudentService",
                column: "UserId",
                principalTable: "UserService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers",
                column: "UserId",
                principalTable: "UserService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "UserService",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "StudentService");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "UserService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "UserService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "UserService",
                newName: "UserService");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RoleEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "UserService",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RoleEntity",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserService",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "UserService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "RoleEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_User_UserId",
                table: "StudentService",
                column: "UserId",
                principalTable: "UserService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_User_UserId",
                table: "Teachers",
                column: "UserId",
                principalTable: "UserService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "UserService",
                column: "RoleId",
                principalTable: "RoleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
