using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationCourseNTier.DataAccess.Migrations
{
    public partial class AddNewPropertyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FileDetail_StudentId",
                table: "FileDetail",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileDetail_Students_StudentId",
                table: "FileDetail",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileDetail_Students_StudentId",
                table: "FileDetail");

            migrationBuilder.DropIndex(
                name: "IX_FileDetail_StudentId",
                table: "FileDetail");
        }
    }
}
