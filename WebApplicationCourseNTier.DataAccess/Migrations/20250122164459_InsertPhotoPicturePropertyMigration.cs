using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationCourseNTier.DataAccess.Migrations
{
    public partial class InsertPhotoPicturePropertyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileDetail_StudentId",
                table: "FileDetail");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "FileDetail");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "FileDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "FileDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FileDetail_StudentId",
                table: "FileDetail",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileDetail_StudentId",
                table: "FileDetail");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "FileDetail");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "FileDetail");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "FileDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_FileDetail_StudentId",
                table: "FileDetail",
                column: "StudentId",
                unique: true);
        }
    }
}
