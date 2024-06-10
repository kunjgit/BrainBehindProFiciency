using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proficiency.Migrations
{
    /// <inheritdoc />
    public partial class analyticsupdated6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProWise");

            migrationBuilder.DropTable(
                name: "SubWise");

            migrationBuilder.DropTable(
                name: "StudentWiseAnalytics");

            migrationBuilder.CreateTable(
                name: "StudAnalytics",
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
                    table.PrimaryKey("PK_StudAnalytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Professor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lectures = table.Column<int>(type: "int", nullable: false),
                    RootAnalyticid = table.Column<int>(type: "int", nullable: true),
                    StudAnalyticId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfAnalytics_RootAnalytics_RootAnalyticid",
                        column: x => x.RootAnalyticid,
                        principalTable: "RootAnalytics",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ProfAnalytics_StudAnalytics_StudAnalyticId",
                        column: x => x.StudAnalyticId,
                        principalTable: "StudAnalytics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lectures = table.Column<int>(type: "int", nullable: false),
                    RootAnalyticid = table.Column<int>(type: "int", nullable: true),
                    StudAnalyticId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAnalytics_RootAnalytics_RootAnalyticid",
                        column: x => x.RootAnalyticid,
                        principalTable: "RootAnalytics",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SubAnalytics_StudAnalytics_StudAnalyticId",
                        column: x => x.StudAnalyticId,
                        principalTable: "StudAnalytics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfAnalytics_RootAnalyticid",
                table: "ProfAnalytics",
                column: "RootAnalyticid");

            migrationBuilder.CreateIndex(
                name: "IX_ProfAnalytics_StudAnalyticId",
                table: "ProfAnalytics",
                column: "StudAnalyticId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAnalytics_RootAnalyticid",
                table: "SubAnalytics",
                column: "RootAnalyticid");

            migrationBuilder.CreateIndex(
                name: "IX_SubAnalytics_StudAnalyticId",
                table: "SubAnalytics",
                column: "StudAnalyticId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfAnalytics");

            migrationBuilder.DropTable(
                name: "SubAnalytics");

            migrationBuilder.DropTable(
                name: "StudAnalytics");

            migrationBuilder.CreateTable(
                name: "StudentWiseAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecentUpate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StuId = table.Column<int>(type: "int", nullable: false),
                    TotalLectures = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWiseAnalytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProWise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lectures = table.Column<int>(type: "int", nullable: false),
                    Professor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootAnalyticsid = table.Column<int>(type: "int", nullable: true),
                    StudentWiseAnalyticsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProWise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProWise_RootAnalytics_RootAnalyticsid",
                        column: x => x.RootAnalyticsid,
                        principalTable: "RootAnalytics",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ProWise_StudentWiseAnalytics_StudentWiseAnalyticsId",
                        column: x => x.StudentWiseAnalyticsId,
                        principalTable: "StudentWiseAnalytics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubWise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lectures = table.Column<int>(type: "int", nullable: false),
                    RootAnalyticsid = table.Column<int>(type: "int", nullable: true),
                    StudentWiseAnalyticsId = table.Column<int>(type: "int", nullable: true),
                    Sub = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubWise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubWise_RootAnalytics_RootAnalyticsid",
                        column: x => x.RootAnalyticsid,
                        principalTable: "RootAnalytics",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SubWise_StudentWiseAnalytics_StudentWiseAnalyticsId",
                        column: x => x.StudentWiseAnalyticsId,
                        principalTable: "StudentWiseAnalytics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProWise_RootAnalyticsid",
                table: "ProWise",
                column: "RootAnalyticsid");

            migrationBuilder.CreateIndex(
                name: "IX_ProWise_StudentWiseAnalyticsId",
                table: "ProWise",
                column: "StudentWiseAnalyticsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubWise_RootAnalyticsid",
                table: "SubWise",
                column: "RootAnalyticsid");

            migrationBuilder.CreateIndex(
                name: "IX_SubWise_StudentWiseAnalyticsId",
                table: "SubWise",
                column: "StudentWiseAnalyticsId");
        }
    }
}
