using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analytics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RootAnalytics",
                columns: table => new
                {
                    version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RootAnalytics");
        }
    }
}
