using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursusApp.Backend.Migrations
{
    public partial class ChangedStartdatumType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Startdatum",
                table: "CursusInstanties",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Startdatum",
                table: "CursusInstanties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
