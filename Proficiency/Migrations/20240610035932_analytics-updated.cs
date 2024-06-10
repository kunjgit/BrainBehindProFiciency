using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analyticsupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "RootAnalytics",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RootAnalytics",
                table: "RootAnalytics",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RootAnalytics",
                table: "RootAnalytics");

            migrationBuilder.DropColumn(
                name: "id",
                table: "RootAnalytics");
        }
    }
}
