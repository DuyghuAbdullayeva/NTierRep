using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplicationCourseNTier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30e29c06-4aec-45fc-ab17-0309e24fab7f", null, "User", "USER" },
                    { "ebacf143-33c9-42a7-bc13-b4c5925b3faf", null, "Admin", "ADMIN" },
                    { "ff9f4595-7e4e-4feb-8319-3096f3413992", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30e29c06-4aec-45fc-ab17-0309e24fab7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebacf143-33c9-42a7-bc13-b4c5925b3faf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff9f4595-7e4e-4feb-8319-3096f3413992");
        }
    }
}
