using Microsoft.EntityFrameworkCore.Migrations;

namespace SmallDad.Data.Migrations
{
    public partial class numvotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumVotes",
                table: "Ranks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumVotes",
                table: "Ranks");
        }
    }
}
