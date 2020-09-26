using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.DAL.Migrations
{
    public partial class UpdatedModelsProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserContacts",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAcquiring",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPublishing",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfCopies",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPages",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BookRentEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRenting",
                table: "BookRentEvents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfReturn",
                table: "BookRentEvents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateToReturn",
                table: "BookRentEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BookRentEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BookId",
                table: "Genres",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserContacts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfAcquiring",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateOfPublishing",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NumberOfCopies",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NumberOfPages",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookRentEvents");

            migrationBuilder.DropColumn(
                name: "DateOfRenting",
                table: "BookRentEvents");

            migrationBuilder.DropColumn(
                name: "DateOfReturn",
                table: "BookRentEvents");

            migrationBuilder.DropColumn(
                name: "DateToReturn",
                table: "BookRentEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookRentEvents");
        }
    }
}
