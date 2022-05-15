using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogyBackend.Migrations
{
    public partial class ondeleteclientcascadeonplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "comments_ibfk_1",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "pinPosts_ibfk_1",
                table: "pinPosts");

            migrationBuilder.AddForeignKey(
                name: "comments_ibfk_1",
                table: "comments",
                column: "username",
                principalTable: "users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "pinPosts_ibfk_1",
                table: "pinPosts",
                column: "username",
                principalTable: "users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "comments_ibfk_1",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "pinPosts_ibfk_1",
                table: "pinPosts");

            migrationBuilder.AddForeignKey(
                name: "comments_ibfk_1",
                table: "comments",
                column: "username",
                principalTable: "users",
                principalColumn: "username");

            migrationBuilder.AddForeignKey(
                name: "pinPosts_ibfk_1",
                table: "pinPosts",
                column: "username",
                principalTable: "users",
                principalColumn: "username");
        }
    }
}
