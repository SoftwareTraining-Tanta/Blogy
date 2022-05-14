using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogyBackend.Migrations
{
    public partial class newmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_Admins_AdminUsername",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_plans_Admins_AdminUsername",
                table: "plans");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_Admins_AdminUsername",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "AdminUsername",
                table: "posts",
                newName: "adminUsername");

            migrationBuilder.RenameIndex(
                name: "IX_posts_AdminUsername",
                table: "posts",
                newName: "IX_posts_adminUsername");

            migrationBuilder.RenameColumn(
                name: "AdminUsername",
                table: "plans",
                newName: "adminUsername");

            migrationBuilder.RenameIndex(
                name: "IX_plans_AdminUsername",
                table: "plans",
                newName: "IX_plans_adminUsername");

            migrationBuilder.RenameColumn(
                name: "AdminUsername",
                table: "comments",
                newName: "adminUsername");

            migrationBuilder.RenameIndex(
                name: "IX_comments_AdminUsername",
                table: "comments",
                newName: "IX_comments_adminUsername");

            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "adminUsername",
                keyValue: null,
                column: "adminUsername",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "adminUsername",
                table: "posts",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddColumn<int>(
                name: "adminId",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "plans",
                keyColumn: "adminUsername",
                keyValue: null,
                column: "adminUsername",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "adminUsername",
                table: "plans",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddColumn<int>(
                name: "adminId",
                table: "plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "comments",
                keyColumn: "adminUsername",
                keyValue: null,
                column: "adminUsername",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "adminUsername",
                table: "comments",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddColumn<int>(
                name: "adminId",
                table: "comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_Admins_adminUsername",
                table: "comments",
                column: "adminUsername",
                principalTable: "Admins",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_plans_Admins_adminUsername",
                table: "plans",
                column: "adminUsername",
                principalTable: "Admins",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_Admins_adminUsername",
                table: "posts",
                column: "adminUsername",
                principalTable: "Admins",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_Admins_adminUsername",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_plans_Admins_adminUsername",
                table: "plans");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_Admins_adminUsername",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "adminId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "adminId",
                table: "plans");

            migrationBuilder.DropColumn(
                name: "adminId",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "adminUsername",
                table: "posts",
                newName: "AdminUsername");

            migrationBuilder.RenameIndex(
                name: "IX_posts_adminUsername",
                table: "posts",
                newName: "IX_posts_AdminUsername");

            migrationBuilder.RenameColumn(
                name: "adminUsername",
                table: "plans",
                newName: "AdminUsername");

            migrationBuilder.RenameIndex(
                name: "IX_plans_adminUsername",
                table: "plans",
                newName: "IX_plans_AdminUsername");

            migrationBuilder.RenameColumn(
                name: "adminUsername",
                table: "comments",
                newName: "AdminUsername");

            migrationBuilder.RenameIndex(
                name: "IX_comments_adminUsername",
                table: "comments",
                newName: "IX_comments_AdminUsername");

            migrationBuilder.AlterColumn<string>(
                name: "AdminUsername",
                table: "posts",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "AdminUsername",
                table: "plans",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "AdminUsername",
                table: "comments",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_Admins_AdminUsername",
                table: "comments",
                column: "AdminUsername",
                principalTable: "Admins",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_plans_Admins_AdminUsername",
                table: "plans",
                column: "AdminUsername",
                principalTable: "Admins",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_Admins_AdminUsername",
                table: "posts",
                column: "AdminUsername",
                principalTable: "Admins",
                principalColumn: "Username");
        }
    }
}
