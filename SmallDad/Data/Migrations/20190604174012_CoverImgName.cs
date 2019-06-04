using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Data.Migrations
{
    public partial class CoverImgName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImgPath",
                table: "Ranks",
                newName: "CoverImgName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImgName",
                table: "Ranks",
                newName: "CoverImgPath");
        }
    }
}
