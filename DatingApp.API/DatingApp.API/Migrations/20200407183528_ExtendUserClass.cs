using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ExtendUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "tblUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "tblUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "tblUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "tblUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "tblUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    URL = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "City",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "tblUser");
        }
    }
}
