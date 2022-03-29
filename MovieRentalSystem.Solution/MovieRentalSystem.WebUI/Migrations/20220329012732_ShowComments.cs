using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class ShowComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShowComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowComments_ShowComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ShowComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShowComments_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowComments_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowComments_CreatedByUserId",
                table: "ShowComments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowComments_ParentId",
                table: "ShowComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowComments_ShowId",
                table: "ShowComments",
                column: "ShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowComments");
        }
    }
}
