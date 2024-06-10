using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analyticsupdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "lectures",
                table: "ProfWiseAnalytics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "prof_name",
                table: "ProfWiseAnalytics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lectures",
                table: "ProfWiseAnalytics");

            migrationBuilder.DropColumn(
                name: "prof_name",
                table: "ProfWiseAnalytics");
        }
    }
}
