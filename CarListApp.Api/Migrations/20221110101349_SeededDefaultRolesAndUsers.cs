using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarListApp.Api.Migrations
{
    public partial class SeededDefaultRolesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "dfc781c0-f39d-4bae-beb6-d9d281513662", "User", "USER" },
                    { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "6a87d373-464f-4640-bfb2-5d84f15cfa6e", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "61a50fbb-041d-4133-bbd2-0be0e369a795", 0, "c7838f75-860e-4de8-b4e8-649e4ff6570b", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEDa3c1N6jF0hi2pHzO+qa9Ok7iDjUFddxQkXYomYfwrVgySWODGvPF+JL31wXPYdhg==", null, false, "5553a85e-3fee-405a-ad2c-760f3cb8a003", false, "admin@localhost.com" },
                    { "7197e409-c2d1-4dcf-b0b0-b32054bbfd8b", 0, "6c6a8b52-645f-438e-92ca-e055cca7c1f4", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAEHbZ7pvUNPVg/WWuitlRq3lwrgY5vQQcR+b6qu2gft7sTfenE/KIF6TGj10DsTrblA==", null, false, "d1c6f05a-8b16-418c-a675-3a3e4fc33086", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "61a50fbb-041d-4133-bbd2-0be0e369a795" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "7197e409-c2d1-4dcf-b0b0-b32054bbfd8b" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cars_Table");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
