using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Infrastructure.Data.Migrations
{
    public partial class UpdateComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ranks_Rankid",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "Rankid",
                table: "Comment",
                newName: "RankId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_Rankid",
                table: "Comment",
                newName: "IX_Comment_RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ranks_RankId",
                table: "Comment",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ranks_RankId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "RankId",
                table: "Comment",
                newName: "Rankid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_RankId",
                table: "Comment",
                newName: "IX_Comment_Rankid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ranks_Rankid",
                table: "Comment",
                column: "Rankid",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
