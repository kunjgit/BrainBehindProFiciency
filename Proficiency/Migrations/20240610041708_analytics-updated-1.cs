using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analyticsupdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "latest_update",
                table: "RootAnalytics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "total_lectures",
                table: "RootAnalytics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProfWiseAnalytics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RootAnalyticsid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfWiseAnalytics", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProfWiseAnalytics_RootAnalytics_RootAnalyticsid",
                        column: x => x.RootAnalyticsid,
                        principalTable: "RootAnalytics",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SubWiseAnalytics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subname = table.Column<string>(name: "sub_name", type: "nvarchar(max)", nullable: false),
                    lectures = table.Column<int>(type: "int", nullable: false),
                    RootAnalyticsid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubWiseAnalytics", x => x.id);
                    table.ForeignKey(
                        name: "FK_SubWiseAnalytics_RootAnalytics_RootAnalyticsid",
                        column: x => x.RootAnalyticsid,
                        principalTable: "RootAnalytics",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfWiseAnalytics_RootAnalyticsid",
                table: "ProfWiseAnalytics",
                column: "RootAnalyticsid");

            migrationBuilder.CreateIndex(
                name: "IX_SubWiseAnalytics_RootAnalyticsid",
                table: "SubWiseAnalytics",
                column: "RootAnalyticsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfWiseAnalytics");

            migrationBuilder.DropTable(
                name: "SubWiseAnalytics");

            migrationBuilder.DropColumn(
                name: "latest_update",
                table: "RootAnalytics");

            migrationBuilder.DropColumn(
                name: "total_lectures",
                table: "RootAnalytics");
        }
    }
}
