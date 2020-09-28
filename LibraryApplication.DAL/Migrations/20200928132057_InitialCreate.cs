using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "Book_seq",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "BookEvent_seq",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "Genre_seq",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "User_seq",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.Book_seq"),
                    Title = table.Column<string>(nullable: true),
                    NumberOfCopies = table.Column<int>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    DateOfPublishing = table.Column<DateTime>(nullable: true),
                    DateOfAcquiring = table.Column<DateTime>(nullable: true),
                    NumberOfPages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.User_seq"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    UserContacts = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.Genre_seq"),
                    Name = table.Column<string>(nullable: true),
                    BookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookRentEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BookEvent_seq"),
                    DateOfRenting = table.Column<DateTime>(nullable: false),
                    NumberOfCopiesRented = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    DateToReturn = table.Column<DateTime>(nullable: false),
                    DateOfReturn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRentEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookRentEvents_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRentEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRentEvents_BookId",
                table: "BookRentEvents",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRentEvents_UserId",
                table: "BookRentEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BookId",
                table: "Genres",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRentEvents");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropSequence(
                name: "Book_seq",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "BookEvent_seq",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "Genre_seq",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "User_seq",
                schema: "dbo");
        }
    }
}
