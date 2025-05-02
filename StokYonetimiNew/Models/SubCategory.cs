using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StokYonetimiNew.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required, MaxLength(4)]
        public string Code { get; set; }    // Örn: "0101"

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int MainCategoryId { get; set; }

        [JsonIgnore]
        public MainCategory? MainCategory { get; set; }

        
    }
}
