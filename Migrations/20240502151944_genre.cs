using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicBackend.Migrations
{
    /// <inheritdoc />
    public partial class genre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Genres",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Genres",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageName", "ImageUrl" },
                values: new object[] { "popgenre.jpg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Fpopgenre.jpg?alt=media&token=1b5fdd62-54bc-4c15-b206-4ee31210c195" });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageName", "ImageUrl", "Name" },
                values: new object[] { "technogenre.png", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Ftechnogenre.png?alt=media&token=660bbeb5-9bfe-4a70-8d14-97f3e7ff4832", "Techno" });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImageName", "ImageUrl", "Name" },
                values: new object[] { "rapgenre.jpg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frapgenre.jpg?alt=media&token=c9ac3e16-f8a0-41b6-a525-a9a826f4e64c", "Rap" });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageName", "ImageUrl" },
                values: new object[] { "rbgenre.jpeg", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frbgenre.jpeg?alt=media&token=58bfa6c2-61fe-49c3-9a3f-55a9ee2080b6" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirebaseId", "FirstName", "LastName", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2000, 1, 1), "artist1@gmail.com", "VqLmNcrN8dfaEuvCGpRQN0INHaM2", "artist1", "artist1", 2 },
                    { 2, new DateOnly(2000, 1, 1), "user1@gmail.com", "EGS0EvbwKpekX3hF01bQXy7Qsdw2", "user1", "user1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "ImageName", "ImageUrl", "Name", "UserId" },
                values: new object[] { 1, "yh5cdnbnj03", "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Artist%2FProfileImage%2F3%2Fyh5cdnbnj03?alt=media&token=21f75e8d-44a1-4e4e-9cc6-259ead7cef08", "artist1", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Genres");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Rock");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Hip-Hop");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Jazz" },
                    { 5, "Classical" },
                    { 6, "Country" },
                    { 7, "Electronic" },
                    { 8, "Folk" },
                    { 10, "Reggae" }
                });
        }
    }
}
