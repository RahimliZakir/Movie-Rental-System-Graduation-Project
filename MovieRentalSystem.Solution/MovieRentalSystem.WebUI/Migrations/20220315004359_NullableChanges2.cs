using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class NullableChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessages_CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessages_DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_CreatedByUserId",
                table: "ContactMessages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_DeletedByUserId",
                table: "ContactMessages",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_Users_DeletedByUserId",
                table: "ContactMessages",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
