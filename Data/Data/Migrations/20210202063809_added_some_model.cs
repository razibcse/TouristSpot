using Microsoft.EntityFrameworkCore.Migrations;

namespace TouristSpot.Data.Migrations
{
    public partial class added_some_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rUIDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rUIDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rUIDs_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "uIDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LikeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uIDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_uIDs_Likes_LikeId",
                        column: x => x.LikeId,
                        principalTable: "Likes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rUIDs_RatingId",
                table: "rUIDs",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_uIDs_LikeId",
                table: "uIDs",
                column: "LikeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rUIDs");

            migrationBuilder.DropTable(
                name: "uIDs");
        }
    }
}
