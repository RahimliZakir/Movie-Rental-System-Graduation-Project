using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class ShowGenreCastItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShowGenreCastItems",
                columns: table => new
                {
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    CastId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowGenreCastItems", x => new { x.ShowId, x.CastId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ShowGenreCastItems_Casts_CastId",
                        column: x => x.CastId,
                        principalTable: "Casts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShowGenreCastItems_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShowGenreCastItems_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShowGenreCastItems_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShowGenreCastItems_Users_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowGenreCastItems_CastId",
                table: "ShowGenreCastItems",
                column: "CastId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowGenreCastItems_CreatedByUserId",
                table: "ShowGenreCastItems",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowGenreCastItems_DeletedByUserId",
                table: "ShowGenreCastItems",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowGenreCastItems_GenreId",
                table: "ShowGenreCastItems",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowGenreCastItems");
        }
    }
}
