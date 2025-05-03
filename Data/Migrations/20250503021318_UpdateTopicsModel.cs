using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNookWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTopicsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts");

            migrationBuilder.AddColumn<string>(
                name: "Responses",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "ForumPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ForumPosts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ForumPosts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedAt",
                table: "ForumPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts");

            migrationBuilder.DropColumn(
                name: "Responses",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "PostedAt",
                table: "ForumPosts");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "ForumPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ForumPosts",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ForumPosts",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId");
        }
    }
}
