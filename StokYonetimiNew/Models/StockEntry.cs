using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class StockEntry
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }       // Tedarik Tarihi

        [Required, MaxLength(50)]
        public string InvoiceNo { get; set; }    // Fatura No

        // FK → Tedarikçi
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        // FK → Ürün
        public int ProductId { get; set; }
        public Material Product { get; set; }

        [Required]
        public decimal Quantity { get; set; }    // Miktar

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }   // Birim Fiyat (KDV H.)
    }
}
