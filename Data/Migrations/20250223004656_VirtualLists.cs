using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlavoursomeWeb.Data.Migrations
{
    public partial class VirtualLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29",
                column: "ConcurrencyStamp",
                value: "db0ae047-7a26-4771-bd99-5e7af929b3ab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a820ccf9-54ac-4047-b4b5-48dab0dc962b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "420339ce-57da-4a3c-b646-2d2fa42b9dee", "AQAAAAEAACcQAAAAEJDgbecmgHhjnBQiT3xwVw783Hjzp5WGpEGnmHjfKnR71OJiV4EQD2ODvdTd4j1RlQ==", "caef404e-764a-45b3-8694-1dcb11e6588f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29",
                column: "ConcurrencyStamp",
                value: "0c9f3720-894d-43e0-a7c7-2ec2f11a3b4a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a820ccf9-54ac-4047-b4b5-48dab0dc962b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a38d9666-71f8-427e-beb0-13075278beb7", "AQAAAAEAACcQAAAAEG08wlPY4oeMoPpSYdPN+csy2lZfQrDNz+fymxucyghEuUShkmwwGmJjPKHOSHk79w==", "468cdbb8-6a91-4705-848d-d8b45c498240" });
        }
    }
}
