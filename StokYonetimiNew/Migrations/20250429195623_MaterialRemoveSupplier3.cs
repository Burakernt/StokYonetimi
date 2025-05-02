using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokYonetimiNew.Migrations
{
    /// <inheritdoc />
    public partial class MaterialRemoveSupplier3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Materials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Materials",
                type: "integer",
                nullable: true);
        }
    }
}
