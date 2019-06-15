using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Infrastructure.Data.Migrations
{
    public partial class AddedCoverImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImgPath",
                table: "Ranks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImgPath",
                table: "Ranks");
        }
    }
}
