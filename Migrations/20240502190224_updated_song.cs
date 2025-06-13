using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBackend.Migrations
{
    /// <inheritdoc />
    public partial class updated_song : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visiblity",
                table: "Songs");

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "ArtistId", "CoverImageName", "CoverImageUrl", "GenreId", "ReleaseDate", "SongName", "SongUrl", "Title" },
                values: new object[] { 1, null, 1, "bz53xifzunh", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Songs%2FCoverImages%2F1%2Fbz53xifzunh?alt=media&token=46bad491-5ec5-44d2-8d27-778b367c883d", 1, new DateOnly(2002, 1, 1), "akow2at5db0", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Songs%2F1%2Fakow2at5db0?alt=media&token=cd090df9-be52-4b97-88f6-9a7030f9cd33", "test1song" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Visiblity",
                table: "Songs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
