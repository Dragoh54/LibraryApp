using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUnqueAuthorSurname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Authors_Surname",
                table: "Authors",
                column: "Surname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_Surname",
                table: "Authors");
        }
    }
}
