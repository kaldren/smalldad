using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Infrastructure.Data.Migrations
{
    public partial class AddedThumbPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoThumbPath",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhotoThumbPath",
                table: "AspNetUsers");
        }
    }
}
