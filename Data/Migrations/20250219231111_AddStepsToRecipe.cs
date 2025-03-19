using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlavoursomeWeb.Data.Migrations
{
    public partial class AddStepsToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Steps_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipeID",
                table: "Steps",
                column: "RecipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23a7ee8-beb5-4238-ad8a-88d54b3c3d29",
                column: "ConcurrencyStamp",
                value: "9d9af99e-e597-4c79-85fe-7b75b38e971e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a820ccf9-54ac-4047-b4b5-48dab0dc962b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "002285c5-f79f-4eec-a532-786af1738d84", "AQAAAAEAACcQAAAAEHqhVSoWv6oJuBoFHRyGPrw8dk5Ep9+jS6k12mECoz3FYeXp7k1kj4EnDMrDcy0L5Q==", "4b8e76d2-f095-4fba-abb6-3d2990f135ba" });
        }
    }
}
