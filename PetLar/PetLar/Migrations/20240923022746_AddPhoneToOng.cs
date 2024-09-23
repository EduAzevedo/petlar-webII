using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetLar.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneToOng : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Ongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Ongs");
        }
    }
}
