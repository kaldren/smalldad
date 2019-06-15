using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Infrastructure.Data.Migrations
{
    public partial class RankAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rankid",
                table: "Comment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Rankid",
                table: "Comment",
                column: "Rankid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ranks_Rankid",
                table: "Comment",
                column: "Rankid",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ranks_Rankid",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Rankid",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Rankid",
                table: "Comment");
        }
    }
}
