using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GibGameDatabase.Migrations
{
    public partial class addigdbid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IgdbGameAndPlatformId",
                table: "GameEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IgdbGameAndPlatformId",
                table: "GameEntries");
        }
    }
}
