using Microsoft.EntityFrameworkCore.Migrations;

namespace IttFelTeheted.API.Migrations
{
    public partial class PostFollowAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostFollows",
                columns: table => new
                {
                    FollowerId = table.Column<int>(nullable: false),
                    FollowedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostFollows", x => new { x.FollowerId, x.FollowedId });
                    table.ForeignKey(
                        name: "FK_PostFollows_Posts_FollowedId",
                        column: x => x.FollowedId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostFollows_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostFollows_FollowedId",
                table: "PostFollows",
                column: "FollowedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostFollows");
        }
    }
}
