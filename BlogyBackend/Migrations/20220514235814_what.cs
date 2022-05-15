using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogyBackend.Migrations
{
    public partial class what : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profilePicture",
                table: "Admins",
                newName: "ProfilePicture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Admins",
                newName: "profilePicture");
        }
    }
}
