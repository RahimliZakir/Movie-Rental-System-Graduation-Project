using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class CreatedAndDeletedByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Faqs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Faqs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessageType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactMessageType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "AppInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "AppInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CreatedByUserId",
                table: "Subscriptions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DeletedByUserId",
                table: "Subscriptions",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_CreatedByUserId",
                table: "Genres",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_DeletedByUserId",
                table: "Genres",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_CreatedByUserId",
                table: "Faqs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_DeletedByUserId",
                table: "Faqs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessageType_CreatedByUserId",
                table: "ContactMessageType",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessageType_DeletedByUserId",
                table: "ContactMessageType",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_CreatedByUserId",
                table: "ContactMessages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_DeletedByUserId",
                table: "ContactMessages",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfos_CreatedByUserId",
                table: "AppInfos",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfos_DeletedByUserId",
                table: "AppInfos",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfos_Users_CreatedByUserId",
                table: "AppInfos",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfos_Users_DeletedByUserId",
                table: "AppInfos",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_Users_DeletedByUserId",
                table: "ContactMessages",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessageType_Users_CreatedByUserId",
                table: "ContactMessageType",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessageType_Users_DeletedByUserId",
                table: "ContactMessageType",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_DeletedByUserId",
                table: "Faqs",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_CreatedByUserId",
                table: "Genres",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_DeletedByUserId",
                table: "Genres",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_CreatedByUserId",
                table: "Subscriptions",
                column: "CreatedByUserId",
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
                name: "FK_AppInfos_Users_CreatedByUserId",
                table: "AppInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInfos_Users_DeletedByUserId",
                table: "AppInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessageType_Users_CreatedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessageType_Users_DeletedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_CreatedByUserId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_DeletedByUserId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_CreatedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CreatedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Genres_CreatedByUserId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_DeletedByUserId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessageType_CreatedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessageType_DeletedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessages_CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropIndex(
                name: "IX_ContactMessages_DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropIndex(
                name: "IX_AppInfos_CreatedByUserId",
                table: "AppInfos");

            migrationBuilder.DropIndex(
                name: "IX_AppInfos_DeletedByUserId",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "AppInfos");
        }
    }
}
