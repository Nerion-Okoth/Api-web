using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api_web1.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "RPG" },
                    { 4, "Strategy" },
                    { 5, "Simulation" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "GenreId", "Name", "Price", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, 1, "Game A", 49.99m, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "Game B", 59.99m, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "Game C", 39.99m, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, "Game D", 29.99m, new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, "Game E", 19.99m, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
