using Microsoft.EntityFrameworkCore.Migrations;

namespace IttFelTeheted.API.Migrations
{
    public partial class ConfirmKeyAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmKey",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmKey",
                table: "Users");
        }
    }
}
