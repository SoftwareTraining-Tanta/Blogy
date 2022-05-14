using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogyBackend.Migrations
{
    public partial class addedissigned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSigned",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSigned",
                table: "users");
        }
    }
}
