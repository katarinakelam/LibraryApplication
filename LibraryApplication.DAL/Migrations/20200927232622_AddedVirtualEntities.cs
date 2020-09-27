using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.DAL.Migrations
{
    public partial class AddedVirtualEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookRentEvents_BookId",
                table: "BookRentEvents",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRentEvents_UserId",
                table: "BookRentEvents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRentEvents_Books_BookId",
                table: "BookRentEvents",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRentEvents_Users_UserId",
                table: "BookRentEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRentEvents_Books_BookId",
                table: "BookRentEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRentEvents_Users_UserId",
                table: "BookRentEvents");

            migrationBuilder.DropIndex(
                name: "IX_BookRentEvents_BookId",
                table: "BookRentEvents");

            migrationBuilder.DropIndex(
                name: "IX_BookRentEvents_UserId",
                table: "BookRentEvents");
        }
    }
}
