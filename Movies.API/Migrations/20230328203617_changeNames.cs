using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.API.Migrations
{
    /// <inheritdoc />
    public partial class changeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movies",
                newName: "Release_Date");

            migrationBuilder.RenameColumn(
                name: "PosterPath",
                table: "Movies",
                newName: "Poster_Path");

            migrationBuilder.RenameColumn(
                name: "OriginalTitle",
                table: "Movies",
                newName: "Original_Title");

            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "Movies",
                newName: "Media_Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Release_Date",
                table: "Movies",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "Poster_Path",
                table: "Movies",
                newName: "PosterPath");

            migrationBuilder.RenameColumn(
                name: "Original_Title",
                table: "Movies",
                newName: "OriginalTitle");

            migrationBuilder.RenameColumn(
                name: "Media_Type",
                table: "Movies",
                newName: "MediaType");
        }
    }
}
