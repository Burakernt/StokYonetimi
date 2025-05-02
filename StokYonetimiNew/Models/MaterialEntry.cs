
using System;
using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class MaterialEntry
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(50)]
        public string InvoiceNumber { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }    

        [Required]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }    

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal? KDV { get; set; } = 20;



    }
}
