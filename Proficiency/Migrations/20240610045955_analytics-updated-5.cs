using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analyticsupdated5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfWiseAnalytics_RootAnalytics_RootAnalyticsid",
                table: "ProfWiseAnalytics");

            migrationBuilder.DropForeignKey(
                name: "FK_SubWiseAnalytics_RootAnalytics_RootAnalyticsid",
                table: "SubWiseAnalytics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubWiseAnalytics",
                table: "SubWiseAnalytics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfWiseAnalytics",
                table: "ProfWiseAnalytics");

            migrationBuilder.RenameTable(
                name: "SubWiseAnalytics",
                newName: "SubWise");

            migrationBuilder.RenameTable(
                name: "ProfWiseAnalytics",
                newName: "ProWise");

            migrationBuilder.RenameIndex(
                name: "IX_SubWiseAnalytics_RootAnalyticsid",
                table: "SubWise",
                newName: "IX_SubWise_RootAnalyticsid");

            migrationBuilder.RenameIndex(
                name: "IX_ProfWiseAnalytics_RootAnalyticsid",
                table: "ProWise",
                newName: "IX_ProWise_RootAnalyticsid");

            migrationBuilder.AddColumn<int>(
                name: "StudentWiseAnalyticsId",
                table: "SubWise",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentWiseAnalyticsId",
                table: "ProWise",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubWise",
                table: "SubWise",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProWise",
                table: "ProWise",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StudentWiseAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuId = table.Column<int>(type: "int", nullable: false),
                    RecentUpate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalLectures = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWiseAnalytics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubWise_StudentWiseAnalyticsId",
                table: "SubWise",
                column: "StudentWiseAnalyticsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProWise_StudentWiseAnalyticsId",
                table: "ProWise",
                column: "StudentWiseAnalyticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProWise_RootAnalytics_RootAnalyticsid",
                table: "ProWise",
                column: "RootAnalyticsid",
                principalTable: "RootAnalytics",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProWise_StudentWiseAnalytics_StudentWiseAnalyticsId",
                table: "ProWise",
                column: "StudentWiseAnalyticsId",
                principalTable: "StudentWiseAnalytics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubWise_RootAnalytics_RootAnalyticsid",
                table: "SubWise",
                column: "RootAnalyticsid",
                principalTable: "RootAnalytics",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubWise_StudentWiseAnalytics_StudentWiseAnalyticsId",
                table: "SubWise",
                column: "StudentWiseAnalyticsId",
                principalTable: "StudentWiseAnalytics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProWise_RootAnalytics_RootAnalyticsid",
                table: "ProWise");

            migrationBuilder.DropForeignKey(
                name: "FK_ProWise_StudentWiseAnalytics_StudentWiseAnalyticsId",
                table: "ProWise");

            migrationBuilder.DropForeignKey(
                name: "FK_SubWise_RootAnalytics_RootAnalyticsid",
                table: "SubWise");

            migrationBuilder.DropForeignKey(
                name: "FK_SubWise_StudentWiseAnalytics_StudentWiseAnalyticsId",
                table: "SubWise");

            migrationBuilder.DropTable(
                name: "StudentWiseAnalytics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubWise",
                table: "SubWise");

            migrationBuilder.DropIndex(
                name: "IX_SubWise_StudentWiseAnalyticsId",
                table: "SubWise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProWise",
                table: "ProWise");

            migrationBuilder.DropIndex(
                name: "IX_ProWise_StudentWiseAnalyticsId",
                table: "ProWise");

            migrationBuilder.DropColumn(
                name: "StudentWiseAnalyticsId",
                table: "SubWise");

            migrationBuilder.DropColumn(
                name: "StudentWiseAnalyticsId",
                table: "ProWise");

            migrationBuilder.RenameTable(
                name: "SubWise",
                newName: "SubWiseAnalytics");

            migrationBuilder.RenameTable(
                name: "ProWise",
                newName: "ProfWiseAnalytics");

            migrationBuilder.RenameIndex(
                name: "IX_SubWise_RootAnalyticsid",
                table: "SubWiseAnalytics",
                newName: "IX_SubWiseAnalytics_RootAnalyticsid");

            migrationBuilder.RenameIndex(
                name: "IX_ProWise_RootAnalyticsid",
                table: "ProfWiseAnalytics",
                newName: "IX_ProfWiseAnalytics_RootAnalyticsid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubWiseAnalytics",
                table: "SubWiseAnalytics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfWiseAnalytics",
                table: "ProfWiseAnalytics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfWiseAnalytics_RootAnalytics_RootAnalyticsid",
                table: "ProfWiseAnalytics",
                column: "RootAnalyticsid",
                principalTable: "RootAnalytics",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubWiseAnalytics_RootAnalytics_RootAnalyticsid",
                table: "SubWiseAnalytics",
                column: "RootAnalyticsid",
                principalTable: "RootAnalytics",
                principalColumn: "id");
        }
    }
}
