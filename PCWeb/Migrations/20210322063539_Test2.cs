using Microsoft.EntityFrameworkCore.Migrations;

namespace PCWeb.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e5fb241-5583-4779-82a8-de99e9274ca6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34cee117-46b4-4523-9b63-f076820b1470");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e64b760a-775f-42e4-9cef-f6e1210dbeb6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4c9673a-80ee-4ab2-9b58-366252f349fd", "2ab8997d-7170-4000-aa45-1814ff2da966", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7cafcbab-9c20-46b9-912a-7bacfd8cb57a", "a5a9ef61-00c2-484c-bbf0-3fc1e49e20b7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f73a45eb-f79b-4eb9-8b7d-42f595df76bd", "709ccdf7-45e4-4940-8d7f-2f0a14bbafcf", "Staff", "STAFF" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cafcbab-9c20-46b9-912a-7bacfd8cb57a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4c9673a-80ee-4ab2-9b58-366252f349fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f73a45eb-f79b-4eb9-8b7d-42f595df76bd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e64b760a-775f-42e4-9cef-f6e1210dbeb6", "ed016e4a-95be-4291-b25f-10a8f5a6a9c7", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34cee117-46b4-4523-9b63-f076820b1470", "9e359980-8923-4a1a-ae1d-2e438b0ecbe8", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e5fb241-5583-4779-82a8-de99e9274ca6", "b3d1f682-c3b0-4123-87ef-826b0dfd1cdc", "Staff", "STAFF" });
        }
    }
}
