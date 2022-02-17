using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GibGameDatabase.Migrations
{
    public partial class addedplayeddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePlayed",
                table: "GameEntries",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePlayed",
                table: "GameEntries");
        }
    }
}
