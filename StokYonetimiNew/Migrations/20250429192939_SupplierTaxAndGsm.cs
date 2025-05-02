using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokYonetimiNew.Migrations
{
    /// <inheritdoc />
    public partial class SupplierTaxAndGsm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Suppliers",
                newName: "MobilePhone");

            migrationBuilder.AddColumn<string>(
                name: "LandlinePhone",
                table: "Suppliers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Suppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandlinePhone",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "MobilePhone",
                table: "Suppliers",
                newName: "Phone");
        }
    }
}
