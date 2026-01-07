using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerServiceCampaign.Modules.Users.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addditionalpremissionsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "users",
                table: "permissions",
                column: "Code",
                values: new object[]
                {
                    "campaigns:create",
                    "campaigns:read",
                    "campaigns:update",
                    "rewards:create",
                    "rewards:read",
                    "rewards:update"
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permissions",
                columns: new[] { "PermissionCode", "RoleName" },
                values: new object[,]
                {
                    { "campaigns:create", "Administrator" },
                    { "campaigns:create", "Operator" },
                    { "campaigns:read", "Administrator" },
                    { "campaigns:read", "Operator" },
                    { "campaigns:update", "Administrator" },
                    { "campaigns:update", "Operator" },
                    { "rewards:create", "Administrator" },
                    { "rewards:create", "Operator" },
                    { "rewards:read", "Administrator" },
                    { "rewards:read", "Operator" },
                    { "rewards:update", "Administrator" },
                    { "rewards:update", "Operator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "campaigns:create", "Administrator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "campaigns:create", "Operator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "campaigns:read", "Administrator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "campaigns:read", "Operator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "campaigns:update", "Administrator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "campaigns:update", "Operator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "rewards:create", "Administrator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "rewards:create", "Operator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "rewards:read", "Administrator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "rewards:read", "Operator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "rewards:update", "Administrator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "PermissionCode", "RoleName" },
                keyValues: new object[] { "rewards:update", "Operator" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "Code",
                keyValue: "campaigns:create");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "Code",
                keyValue: "campaigns:read");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "Code",
                keyValue: "campaigns:update");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "Code",
                keyValue: "rewards:create");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "Code",
                keyValue: "rewards:read");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "Code",
                keyValue: "rewards:update");
        }
    }
}
