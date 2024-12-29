using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initialWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Country = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    WhenUsed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ISBN = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Genre = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TakenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnBy = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "Country", "Surname" },
                values: new object[,]
                {
                    { new Guid("2131a841-f799-4f91-abef-6790014f5b66"), new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "United Kingdom", "Austen" },
                    { new Guid("41030ea2-ab9c-417e-a926-59e4b0f895de"), new DateTime(1948, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), "United States", "Martin" },
                    { new Guid("baca402e-d45d-461a-bd80-4661057bcc7a"), new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "United Kingdom", "Tolkien" },
                    { new Guid("ce968f77-3e8f-46f7-a500-2c665e470718"), new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "United Kingdom", "Orwell" },
                    { new Guid("eda3a697-7301-4a55-898c-abdf7419e9e8"), new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc), "United Kingdom", "Rowling" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nickname", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("38a861d3-919e-4934-a6f0-27e28fb1fff4"), "historybuff@example.com", "historybuff", "$2a$11$JVf9op3AIrqSFW8Jw3i2GuTAN7prWd3mjKiqj8nZl3B7RTmBduRO6", 1 },
                    { new Guid("3fb45498-0311-4ed6-85be-333656ed177a"), "sciencegeek@example.com", "sciencegeek", "$2a$11$x8GcliIGZ63TvuCKS.iPgeXsXwhIp0Q9uaXpA8FPhsfhlKtfft.s.", 1 },
                    { new Guid("7f77794a-1409-4281-abea-7ba9995a6c07"), "booklover123@example.com", "booklover123", "$2a$11$OU0YC58MwgjsyeJkx9MzDOTzFvxNJAaCVmwxfzFta3m4ZkSR/Jili", 1 },
                    { new Guid("91aabcf3-662b-41e1-a6ed-312496c55940"), "fan@example.com", "literaturefan", "$2a$11$MMAXtpHuGJw0Opl2N8Ws7.vPzTYRf2Dptjc1U8zrpdlWNzJJTZoYy", 1 },
                    { new Guid("dd47b691-3910-48a7-b76d-f67602195752"), "admin@example.com", "adminuser", "$2a$11$rYWcqxJlCbKbMcSRhwB1..um354tOhZ3mRetzXj43tLyj6AU97uoO", 2 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Genre", "ISBN", "ReturnBy", "TakenAt", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("1423397e-3ca8-4164-b479-ae1fec105185"), new Guid("41030ea2-ab9c-417e-a926-59e4b0f895de"), "The first book in the A Song of Ice and Fire series by George R.R. Martin.", "Fantasy", "978-0553103540", new DateTime(2025, 1, 3, 22, 37, 50, 260, DateTimeKind.Utc).AddTicks(8111), new DateTime(2024, 12, 9, 22, 37, 50, 260, DateTimeKind.Utc).AddTicks(8110), "A Game of Thrones", new Guid("91aabcf3-662b-41e1-a6ed-312496c55940") },
                    { new Guid("373ccaf8-9151-4a40-9657-647eb0ab7e13"), new Guid("2131a841-f799-4f91-abef-6790014f5b66"), "A classic novel by Jane Austen.", "Classic", "978-0141439518", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pride and Prejudice", null },
                    { new Guid("658b45b5-f562-4760-b26a-20fc95ba744a"), new Guid("ce968f77-3e8f-46f7-a500-2c665e470718"), "A dystopian novel by George Orwell.", "Dystopian", "978-0451524935", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984", null },
                    { new Guid("a7d05602-f3cf-41f5-953f-1f80ca913b6a"), new Guid("baca402e-d45d-461a-bd80-4661057bcc7a"), "A fantasy novel by J.R.R. Tolkien.", "Fantasy", "978-0261103573", new DateTime(2025, 1, 18, 22, 37, 50, 260, DateTimeKind.Utc).AddTicks(8069), new DateTime(2024, 12, 19, 22, 37, 50, 260, DateTimeKind.Utc).AddTicks(8065), "The Hobbit", new Guid("7f77794a-1409-4281-abea-7ba9995a6c07") },
                    { new Guid("ad4e9ec3-adaf-46d9-9c00-6ce7911cd786"), new Guid("eda3a697-7301-4a55-898c-abdf7419e9e8"), "The first book in the Harry Potter series by J.K. Rowling.", "Fantasy", "978-0747532743", new DateTime(2025, 1, 13, 22, 37, 50, 260, DateTimeKind.Utc).AddTicks(8076), new DateTime(2024, 12, 24, 22, 37, 50, 260, DateTimeKind.Utc).AddTicks(8075), "Harry Potter and the Philosopher's Stone", new Guid("7f77794a-1409-4281-abea-7ba9995a6c07") },
                    { new Guid("dba2fcaa-7302-497a-a3cc-c5efbb6f2ae9"), new Guid("baca402e-d45d-461a-bd80-4661057bcc7a"), "An epic high-fantasy novel by J.R.R. Tolkien.", "Fantasy", "978-0261102385", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings", null },
                    { new Guid("e4a8e749-7e9c-4637-977c-add712f999db"), new Guid("eda3a697-7301-4a55-898c-abdf7419e9e8"), "The sixth book in the Harry Potter series by J.K. Rowling.", "Fantasy", "978-0747581086", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Half-Blood Prince", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Surname",
                table: "Authors",
                column: "Surname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
