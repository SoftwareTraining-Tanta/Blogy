using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogyBackend.Migrations
{
    public partial class onpostdeletecascade1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "comments_ibfk_2",
                table: "comments");

            migrationBuilder.AddForeignKey(
                name: "comments_ibfk_2",
                table: "comments",
                column: "postId",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "comments_ibfk_2",
                table: "comments");

            migrationBuilder.AddForeignKey(
                name: "comments_ibfk_2",
                table: "comments",
                column: "postId",
                principalTable: "posts",
                principalColumn: "id");
        }
    }
}
