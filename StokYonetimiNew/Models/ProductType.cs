using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StokYonetimiNew.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required, MaxLength(7)]
        public string Code { get; set; }   // Örn: "0101001"

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [JsonIgnore]
        public SubCategory? SubCategory { get; set; }
    }
}
