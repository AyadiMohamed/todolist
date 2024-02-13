using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Migrations
{
    /// <inheritdoc />
    public partial class update_nav_prop_make_userId_null_in_member : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AbpUsers_UserId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_MemberId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_MemberId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Members",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AbpUsers_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AbpUsers_UserId",
                table: "Members");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId1",
                table: "Tasks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Members",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MemberId1",
                table: "Tasks",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AbpUsers_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_MemberId1",
                table: "Tasks",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
