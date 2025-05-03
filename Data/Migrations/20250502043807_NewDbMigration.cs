using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNookWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Topics_TopicId",
                table: "ForumPosts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
