using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFileSystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "36a3df67-dcd8-48ab-9879-e6a083e9125a", "c534eebe-2228-4a94-8bb4-1120e91c9525" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a3df67-dcd8-48ab-9879-e6a083e9125a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c534eebe-2228-4a94-8bb4-1120e91c9525");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd92068c-5d8d-4dbf-b3f4-0cb0e082c2a3", "71df6084-fc9d-4790-8149-03d20cdff99f", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "911515a3-3ae7-4390-8aae-9fc26f6e40d3", 0, "4a662e90-fb98-4b88-b558-ef2da4038406", "developer@gmail.com", true, false, null, "DEVELOPER@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEG/uDlX/f78Kq1pzOIKsG9+k+D9piAvG7VD+5/fjb+LjAQSoa2Nn8xi3dtOh7kxbxg==", "+962788000000000", true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fd92068c-5d8d-4dbf-b3f4-0cb0e082c2a3", "911515a3-3ae7-4390-8aae-9fc26f6e40d3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fd92068c-5d8d-4dbf-b3f4-0cb0e082c2a3", "911515a3-3ae7-4390-8aae-9fc26f6e40d3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd92068c-5d8d-4dbf-b3f4-0cb0e082c2a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "911515a3-3ae7-4390-8aae-9fc26f6e40d3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36a3df67-dcd8-48ab-9879-e6a083e9125a", "91bad459-587e-42be-b565-ade05756ad90", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c534eebe-2228-4a94-8bb4-1120e91c9525", 0, "86278092-15a0-476f-9d5e-39ca03ce0089", "developer@gmail.com", true, false, null, "DEVELOPER@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAENCsodwIwjWZOHrsXrezIKsDLO73FStmA++l3pvmqEtRWyOzwUvW0yahedcmBMTD7g==", "+962788000000000", true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "36a3df67-dcd8-48ab-9879-e6a083e9125a", "c534eebe-2228-4a94-8bb4-1120e91c9525" });
        }
    }
}
