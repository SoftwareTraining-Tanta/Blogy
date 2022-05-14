using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogyBackend.Migrations
{
    public partial class modifiedimagetobelongblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Admins",
                newName: "profilePicture");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image",
                table: "posts",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "blob",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profilePicture",
                table: "Admins",
                newName: "ProfilePicture");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image",
                table: "posts",
                type: "blob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true);
        }
    }
}
