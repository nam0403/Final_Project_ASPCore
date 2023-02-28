using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Project_ASPCore.Migrations
{
    /// <inheritdoc />
    public partial class addtablecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Folders");
        }
    }
}
