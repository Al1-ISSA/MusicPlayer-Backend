using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBackend.Migrations
{
    /// <inheritdoc />
    public partial class updated_artist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genres_GenreId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_GenreId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Artists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Artists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_GenreId",
                table: "Artists",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genres_GenreId",
                table: "Artists",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
