using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.Service.Data.Migrations
{
    public partial class AddImageFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Recipes");
        }
    }
}
