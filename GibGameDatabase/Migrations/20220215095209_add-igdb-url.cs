using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GibGameDatabase.Migrations
{
    public partial class addigdburl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IgdbUrl",
                table: "GameEntries",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IgdbUrl",
                table: "GameEntries");
        }
    }
}
