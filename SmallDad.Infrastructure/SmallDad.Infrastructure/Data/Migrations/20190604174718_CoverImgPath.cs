using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Infrastructure.Data.Migrations
{
    public partial class CoverImgPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImgName",
                table: "Ranks",
                newName: "CoverImgPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImgPath",
                table: "Ranks",
                newName: "CoverImgName");
        }
    }
}
