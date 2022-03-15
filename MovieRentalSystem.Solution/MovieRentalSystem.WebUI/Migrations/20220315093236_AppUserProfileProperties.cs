using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRentalSystem.WebUI.Migrations
{
    public partial class AppUserProfileProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "Membership",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobName",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobName",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                schema: "Membership",
                table: "Users");
        }
    }
}
