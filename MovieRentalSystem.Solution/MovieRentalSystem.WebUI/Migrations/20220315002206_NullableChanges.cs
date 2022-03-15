using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class NullableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfos_Users_CreatedByUserId",
                table: "AppInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessageType_Users_CreatedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_CreatedByUserId",
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

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessageType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "AppInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfos_Users_CreatedByUserId",
                table: "AppInfos",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessageType_Users_CreatedByUserId",
                table: "ContactMessageType",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_CreatedByUserId",
                table: "Genres",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfos_Users_CreatedByUserId",
                table: "AppInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_Users_CreatedByUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessageType_Users_CreatedByUserId",
                table: "ContactMessageType");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_CreatedByUserId",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_CreatedByUserId",
                table: "Genres");

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

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Genres",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Faqs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessageType",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "ContactMessages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "AppInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CreatedByUserId",
                table: "Subscriptions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DeletedByUserId",
                table: "Subscriptions",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfos_Users_CreatedByUserId",
                table: "AppInfos",
                column: "CreatedByUserId",
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
                name: "FK_ContactMessageType_Users_CreatedByUserId",
                table: "ContactMessageType",
                column: "CreatedByUserId",
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
                name: "FK_Genres_Users_CreatedByUserId",
                table: "Genres",
                column: "CreatedByUserId",
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
    }
}
