using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationCourseNTier.DataAccess.Migrations
{
    public partial class NewMIg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Students");
        }
    }
}
