using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MusicBackend.Migrations
{
    /// <inheritdoc />
    public partial class recentlyplayed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.CreateTable(
                name: "RecentlyPlayed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SongId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentlyPlayed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecentlyPlayed_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecentlyPlayed_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageName", "ImageUrl", "Name" },
                values: new object[] { "rbgenre.jpeg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frbgenre.jpeg?alt=media&token=58bfa6c2-61fe-49c3-9a3f-55a9ee2080b6", "R&B" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "ImageName", "ImageUrl", "Name" },
                values: new object[] { 2, "rapgenre.jpg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frapgenre.jpg?alt=media&token=c9ac3e16-f8a0-41b6-a525-a9a826f4e64c", "Rap" });

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyPlayed_SongId",
                table: "RecentlyPlayed",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentlyPlayed_UserId",
                table: "RecentlyPlayed",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "RecentlyPlayed");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageName", "ImageUrl", "Name" },
                values: new object[] { "rapgenre.jpg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frapgenre.jpg?alt=media&token=c9ac3e16-f8a0-41b6-a525-a9a826f4e64c", "Rap" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "ImageName", "ImageUrl", "Name" },
                values: new object[] { 9, "rbgenre.jpeg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frbgenre.jpeg?alt=media&token=58bfa6c2-61fe-49c3-9a3f-55a9ee2080b6", "R&B" });

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");
        }
    }
}
