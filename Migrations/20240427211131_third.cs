using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBackend.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Songs",
                newName: "SongUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Songs",
                newName: "SongName");

            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Albums",
                newName: "CoverImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageName",
                table: "Songs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Songs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageName",
                table: "Albums",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageName",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "CoverImageName",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "SongUrl",
                table: "Songs",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "SongName",
                table: "Songs",
                newName: "CoverImage");

            migrationBuilder.RenameColumn(
                name: "CoverImageUrl",
                table: "Albums",
                newName: "CoverImage");
        }
    }
}
