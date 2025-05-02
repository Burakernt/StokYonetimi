using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokYonetimiNew.Migrations
{
    /// <inheritdoc />
    public partial class KDV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "KDV",
                table: "MaterialEntries",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KDV",
                table: "MaterialEntries");
        }
    }
}
