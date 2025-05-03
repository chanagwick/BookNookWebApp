using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNookWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ForumPosts_ForumPostId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ForumPosts_ForumPostId",
                table: "Comments",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "ForumPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ForumPosts_ForumPostId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ForumPosts_ForumPostId",
                table: "Comments",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "ForumPostId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
