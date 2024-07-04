using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommWeb.Migrations
{
    /// <inheritdoc />
    public partial class Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Product",
                newName: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Product",
                newName: "Id");
        }
    }
}
