using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.DAL.Migrations
{
    public partial class AddedBookRentEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "BookEvent_seq",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "BookRentEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BookEvent_seq")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRentEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRentEvents");

            migrationBuilder.DropSequence(
                name: "BookEvent_seq",
                schema: "dbo");
        }
    }
}
