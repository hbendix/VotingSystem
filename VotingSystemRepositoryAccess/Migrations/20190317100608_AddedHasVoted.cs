using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystemRepositoryAccess.Migrations
{
    public partial class AddedHasVoted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HasVoted",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ElectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HasVoted", x => new { x.ElectionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_HasVoted_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HasVoted_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HasVoted_UserId",
                table: "HasVoted",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HasVoted");
        }
    }
}
