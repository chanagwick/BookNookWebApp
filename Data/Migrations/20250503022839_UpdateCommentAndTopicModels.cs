using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNookWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommentAndTopicModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responses",
                table: "Topics");

            migrationBuilder.AlterColumn<int>(
                name: "ForumPostId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TopicId",
                table: "Comments",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Topics_TopicId",
                table: "Comments",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Topics_TopicId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TopicId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Responses",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ForumPostId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
