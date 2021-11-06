using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.API.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a7d020ff-33bf-472f-be13-d41c8487efe8", "ad1e4a60-ca0b-40ea-9977-b2561b365acf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04a0b916-3e65-433b-abf8-d87a3b49fae2", "d0ed7e0c-5d16-4cc6-a77f-e1202cac7bed", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04a0b916-3e65-433b-abf8-d87a3b49fae2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7d020ff-33bf-472f-be13-d41c8487efe8");
        }
    }
}
