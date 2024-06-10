using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analyticsupdated3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lectures",
                table: "SubWiseAnalytics",
                newName: "Lectures");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SubWiseAnalytics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "sub_name",
                table: "SubWiseAnalytics",
                newName: "Sub");

            migrationBuilder.RenameColumn(
                name: "version",
                table: "RootAnalytics",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "total_lectures",
                table: "RootAnalytics",
                newName: "TotalLectures");

            migrationBuilder.RenameColumn(
                name: "latest_update",
                table: "RootAnalytics",
                newName: "LatestUpdate");

            migrationBuilder.RenameColumn(
                name: "lectures",
                table: "ProfWiseAnalytics",
                newName: "Lectures");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProfWiseAnalytics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "prof_name",
                table: "ProfWiseAnalytics",
                newName: "Professor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lectures",
                table: "SubWiseAnalytics",
                newName: "lectures");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SubWiseAnalytics",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Sub",
                table: "SubWiseAnalytics",
                newName: "sub_name");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "RootAnalytics",
                newName: "version");

            migrationBuilder.RenameColumn(
                name: "TotalLectures",
                table: "RootAnalytics",
                newName: "total_lectures");

            migrationBuilder.RenameColumn(
                name: "LatestUpdate",
                table: "RootAnalytics",
                newName: "latest_update");

            migrationBuilder.RenameColumn(
                name: "Lectures",
                table: "ProfWiseAnalytics",
                newName: "lectures");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProfWiseAnalytics",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Professor",
                table: "ProfWiseAnalytics",
                newName: "prof_name");
        }
    }
}
