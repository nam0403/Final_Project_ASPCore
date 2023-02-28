using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Project_ASPCore.Migrations
{
    /// <inheritdoc />
    public partial class addmore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Files");
        }
    }
}
