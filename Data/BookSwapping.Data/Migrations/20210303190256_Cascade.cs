using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSwapping.Data.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedBooks_Books_BookId",
                table: "RequestedBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedBooks_Books_BookId",
                table: "RequestedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedBooks_Books_BookId",
                table: "RequestedBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedBooks_Books_BookId",
                table: "RequestedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
