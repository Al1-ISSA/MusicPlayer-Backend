using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBackend.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "ImageName", "ImageUrl", "Name" },
                values: new object[] { 2, "technogenre.png", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Ftechnogenre.png?alt=media&token=660bbeb5-9bfe-4a70-8d14-97f3e7ff4832", "Techno" });
        }
    }
}
