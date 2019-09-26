using Microsoft.EntityFrameworkCore.Migrations;

namespace WebKorpa.Data.Migrations
{
    public partial class Prva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Drzava",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Drzava",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Grad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "AspNetUsers");
        }
    }
}
