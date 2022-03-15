using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class NullabelChanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DeletedByUserId",
                table: "Subscriptions",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_DeletedByUserId",
                table: "ContactMessages",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_Users_DeletedByUserId",
                table: "ContactMessages",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_DeletedByUserId",
                table: "Subscriptions",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessages_DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactMessages");
        }
    }
}
