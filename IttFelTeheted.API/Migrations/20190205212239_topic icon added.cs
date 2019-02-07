using Microsoft.EntityFrameworkCore.Migrations;

namespace IttFelTeheted.API.Migrations
{
    public partial class topiciconadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Topics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Topics");
        }
    }
}
