using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Migrations
{
    /// <inheritdoc />
    public partial class AdjustMemberTaskRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Tasks_Id",
                table: "Members");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId1",
                table: "Tasks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MemberId1",
                table: "Tasks",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_MemberId1",
                table: "Tasks",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_MemberId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_MemberId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Tasks_Id",
                table: "Members",
                column: "Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
