
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StokYonetimiNew.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Code { get; set; } = null!;   // "0101001-001" formatı

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        public int MaterialTypeId { get; set; }

        
        [JsonIgnore]
        public ProductType? MaterialType { get; set; }

       

       
        [JsonIgnore]
        public Supplier? Supplier { get; set; }

        [Required, MaxLength(20)]
        public string Unit { get; set; } = "Adet";

        
        

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int MinStockLevel { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
