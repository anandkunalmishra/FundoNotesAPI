using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoteText",
                table: "NotesTable",
                newName: "NoteTitle");

            migrationBuilder.AddColumn<string>(
                name: "NoteDescription",
                table: "NotesTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteDescription",
                table: "NotesTable");

            migrationBuilder.RenameColumn(
                name: "NoteTitle",
                table: "NotesTable",
                newName: "NoteText");
        }
    }
}
