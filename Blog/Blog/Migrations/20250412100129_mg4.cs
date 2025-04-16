using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogproject.Migrations
{
    /// <inheritdoc />
    public partial class mg4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "X",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "X",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "FirstName");
        }
    }
}
