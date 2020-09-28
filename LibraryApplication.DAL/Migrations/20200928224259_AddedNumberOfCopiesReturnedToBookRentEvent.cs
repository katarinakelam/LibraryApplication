using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.DAL.Migrations
{
    public partial class AddedNumberOfCopiesReturnedToBookRentEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfCopiesReturned",
                table: "BookRentEvents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfCopiesReturned",
                table: "BookRentEvents");
        }
    }
}
